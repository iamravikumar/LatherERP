﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.DatabaseAccessLayer.DB;
using ERP.DatabaseAccessLayer.Utility;
using ERP.EntitiesModel.AppSetupModel;
using ERP.DatabaseAccessLayer.AppSetupGateway;
using ERP.EntitiesModel.BaseModel;
using ERP.EntitiesModel.OperationModel;
using System.Transactions;


namespace ERP.DatabaseAccessLayer.OperationGateway
{
    public class DalCrustingProductionSchedule
    {
        private readonly BLC_DEVEntities _context;
        private ValidationMsg _vmMsg;
        public long YearMonID = 0;
        public long ScheduleID = 0;
        public string ScheduleNo = string.Empty;
        public long ScheduleDateID = 0;
        public string ProductionNo = string.Empty;
        public long ScheduleItemID = 0;
        public long SdulItemColorID = 0;
        public string ScheduleProductionNo = string.Empty;
        private int stockError = 0;
        public DalCrustingProductionSchedule()
        {
            _context = new BLC_DEVEntities();
        }

        public ValidationMsg Save(PrdCrustYearMonth model, int userid, string pageUrl)
        {
            _vmMsg = new ValidationMsg();
            try
            {
                using (var tx = new TransactionScope())
                {
                    using (_context)
                    {
                        model.ScheduleNo = DalCommon.GetPreDefineNextCodeByUrl(pageUrl);//DalCommon.GetPreDefineValue("1", "00045");
                        if (model.ScheduleNo != null)
                        {
                            #region YearMonth

                            PRD_YearMonth tblYearMonth = SetToModelObject(model, userid);
                            _context.PRD_YearMonth.Add(tblYearMonth);
                            _context.SaveChanges();

                            #endregion

                            #region YearMonthSchedule

                            model.YearMonID = tblYearMonth.YearMonID;
                            //model.ScheduleNo = DalCommon.GetPreDefineNextCodeByUrl(pageUrl);//DalCommon.GetPreDefineValue("1", "00045");
                            PRD_YearMonthSchedule tblYearMonthSchedule = SetToYearMonthScheduleModelObject(model, userid);
                            _context.PRD_YearMonthSchedule.Add(tblYearMonthSchedule);
                            _context.SaveChanges();

                            #endregion

                            #region YearMonthScheduleDate

                            model.ScheduleID = tblYearMonthSchedule.ScheduleID;
                            model.ProductionStatus = "Schedule";
                            model.ProductionNo = DalCommon.GetPreDefineNextCodeByUrl("ChemicalConsumption/ChemConsumption");
                            PRD_YearMonthScheduleDate tblPrdYearMonthScheduleDate = SetToScheduleDateModelObject(model, userid);
                            _context.PRD_YearMonthScheduleDate.Add(tblPrdYearMonthScheduleDate);
                            _context.SaveChanges();

                            #endregion

                            #region Save YearMonthCrustScheduleItem List

                            if (model.PrdYearMonthCrustScheduleItemList != null)
                            {
                                foreach (PrdYearMonthCrustScheduleItem objCrustScheduleItem in model.PrdYearMonthCrustScheduleItemList)
                                {
                                    objCrustScheduleItem.ScheduleDateID = tblPrdYearMonthScheduleDate.ScheduleDateID;
                                    objCrustScheduleItem.ScheduleProductionNo = DalCommon.GetPreDefineNextCodeByUrl("ChemicalConsumption/ChemConsumption");
                                    PRD_YearMonthCrustScheduleItem tblCrustScheduleItem = SetToModelObject(objCrustScheduleItem, userid);
                                    _context.PRD_YearMonthCrustScheduleItem.Add(tblCrustScheduleItem);
                                    _context.SaveChanges();
                                    ScheduleItemID = tblCrustScheduleItem.ScheduleItemID;
                                    ScheduleProductionNo = objCrustScheduleItem.ScheduleProductionNo;

                                    #region Save YearMonthCrustScheduleColor Records

                                    if (model.PrdYearMonthCrustScheduleColorList != null)
                                    {
                                        foreach (PrdYearMonthCrustScheduleColor objCrustScheduleColor in model.PrdYearMonthCrustScheduleColorList)
                                        {
                                            objCrustScheduleColor.ScheduleItemID = tblCrustScheduleItem.ScheduleItemID;
                                            objCrustScheduleColor.ProductionStatus = "Schedule";
                                            PRD_YearMonthCrustScheduleColor tblCrustScheduleColor = SetToModelObject(objCrustScheduleColor, userid);
                                            _context.PRD_YearMonthCrustScheduleColor.Add(tblCrustScheduleColor);
                                            _context.SaveChanges();

                                            #region Save YearMonthCrustScheduleDrum List

                                            if (model.PrdYearMonthCrustScheduleDrumList != null)
                                            {
                                                if (objCrustScheduleColor.ColorID == model.PrdYearMonthCrustScheduleDrumList[0].ColorID)
                                                {
                                                    foreach (PrdYearMonthCrustScheduleDrum objCrustScheduleDrum in model.PrdYearMonthCrustScheduleDrumList)
                                                    {
                                                        objCrustScheduleDrum.SdulItemColorID = tblCrustScheduleColor.SdulItemColorID;
                                                        PRD_YearMonthCrustScheduleDrum tblCrustScheduleDrum = SetToModelObject(objCrustScheduleDrum, userid);
                                                        _context.PRD_YearMonthCrustScheduleDrum.Add(tblCrustScheduleDrum);
                                                    }
                                                }
                                            }

                                            #endregion
                                        }
                                    }

                                    #endregion
                                }
                            }

                            #endregion

                            _context.SaveChanges();
                            tx.Complete();
                            YearMonID = tblYearMonth.YearMonID;

                            ScheduleID = tblYearMonthSchedule.ScheduleID;
                            ScheduleNo = model.ScheduleNo;

                            ScheduleDateID = tblPrdYearMonthScheduleDate.ScheduleDateID;
                            ProductionNo = model.ProductionNo;

                            _vmMsg.Type = Enums.MessageType.Success;
                            _vmMsg.Msg = "Saved Successfully.";
                        }
                        else
                        {
                            _vmMsg.Type = Enums.MessageType.Error;
                            _vmMsg.Msg = "ScheduleNo Predefine Value not Found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _vmMsg.Type = Enums.MessageType.Error;
                _vmMsg.Msg = "Failed to save.";
                if (ex.InnerException.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    _vmMsg.Type = Enums.MessageType.Error;
                    _vmMsg.Msg = "Schedule Year,Month Combination Data Already Exit.";
                }
            }
            return _vmMsg;
        }

        public ValidationMsg Update(PrdCrustYearMonth model, int userid)
        {
            _vmMsg = new ValidationMsg();
            try
            {
                #region Update

                using (var tx = new TransactionScope())
                {
                    using (_context)
                    {
                        #region YearMonth

                        PRD_YearMonth CurrentEntity = SetToModelObject(model, userid);
                        var OriginalEntity = _context.PRD_YearMonth.First(m => m.YearMonID == model.YearMonID);

                        OriginalEntity.ScheduleYear = CurrentEntity.ScheduleYear;
                        OriginalEntity.ScheduleMonth = CurrentEntity.ScheduleMonth;
                        OriginalEntity.ScheduleFor = CurrentEntity.ScheduleFor;
                        OriginalEntity.ProductionFloor = CurrentEntity.ProductionFloor;
                        //OriginalEntity.ConcernStore = CurrentEntity.ConcernStore;
                        OriginalEntity.ModifiedBy = userid;
                        OriginalEntity.ModifiedOn = DateTime.Now;

                        #endregion

                        #region Schedule

                        if (model.ScheduleID == 0)
                        {
                            model.YearMonID = model.YearMonID;
                            model.ScheduleNo = DalCommon.GetPreDefineNextCodeByUrl("WetBlueProductionSchedule/WetBlueProductionSchedule");
                            PRD_YearMonthSchedule tblYearMonthSchedule = SetToYearMonthScheduleModelObject(model, userid);
                            _context.PRD_YearMonthSchedule.Add(tblYearMonthSchedule);
                            _context.SaveChanges();
                            ScheduleID = tblYearMonthSchedule.ScheduleID;
                        }
                        else
                        {
                            PRD_YearMonthSchedule CurrentScheduleEntity = SetToYearMonthScheduleModelObject(model, userid);
                            var OriginalScheduleEntity = _context.PRD_YearMonthSchedule.First(m => m.ScheduleID == model.ScheduleID);

                            OriginalScheduleEntity.PrepareDate = CurrentScheduleEntity.PrepareDate;
                            OriginalScheduleEntity.ProductionProcessID = CurrentScheduleEntity.ProductionProcessID;
                            OriginalScheduleEntity.ModifiedBy = userid;
                            OriginalScheduleEntity.ModifiedOn = DateTime.Now;
                            ScheduleID = model.ScheduleID;
                        }

                        #endregion

                        #region Schedule Date

                        if (model.ScheduleDateID == 0)
                        {
                            model.ScheduleID = ScheduleID;
                            model.ProductionStatus = "Schedule";
                            model.ProductionNo = DalCommon.GetPreDefineNextCodeByUrl("ChemicalConsumption/ChemConsumption");

                            PRD_YearMonthScheduleDate tblYearMonthSchedule = SetToScheduleDateModelObject(model, userid);
                            _context.PRD_YearMonthScheduleDate.Add(tblYearMonthSchedule);
                            _context.SaveChanges();
                            ScheduleDateID = tblYearMonthSchedule.ScheduleDateID;

                        }
                        else
                        {
                            PRD_YearMonthScheduleDate CurrentScheduleEntity = SetToScheduleDateModelObject(model, userid);
                            var OriginalScheduleEntity = _context.PRD_YearMonthScheduleDate.First(m => m.ScheduleDateID == model.ScheduleDateID);

                            OriginalScheduleEntity.ScheduleStartDate = CurrentScheduleEntity.ScheduleStartDate;
                            OriginalScheduleEntity.ScheduleEndDate = CurrentScheduleEntity.ScheduleEndDate;
                            OriginalScheduleEntity.ProductionStatus = CurrentScheduleEntity.ProductionStatus;
                            OriginalScheduleEntity.ModifiedBy = userid;
                            OriginalScheduleEntity.ModifiedOn = DateTime.Now;

                            ScheduleDateID = model.ScheduleDateID;
                            ProductionNo = model.ProductionNo;
                        }

                        #endregion

                        #region YearMonthSchedule Item,Color & Grade List

                        if (model.PrdYearMonthCrustScheduleItemList != null)
                        {
                            foreach (PrdYearMonthCrustScheduleItem objCrustScheduleItem in model.PrdYearMonthCrustScheduleItemList)
                            {
                                if (objCrustScheduleItem.ScheduleItemID == 0)
                                {
                                    objCrustScheduleItem.ScheduleDateID = ScheduleDateID;
                                    objCrustScheduleItem.ScheduleProductionNo = DalCommon.GetPreDefineNextCodeByUrl("ChemicalConsumption/ChemConsumption");

                                    PRD_YearMonthCrustScheduleItem tblCrustScheduleItem = SetToModelObject(objCrustScheduleItem, userid);
                                    _context.PRD_YearMonthCrustScheduleItem.Add(tblCrustScheduleItem);
                                    _context.SaveChanges();
                                    ScheduleItemID = tblCrustScheduleItem.ScheduleItemID;
                                    ScheduleProductionNo = objCrustScheduleItem.ScheduleProductionNo;
                                }
                                else
                                {
                                    PRD_YearMonthCrustScheduleItem CurrEntity = SetToModelObject(objCrustScheduleItem, userid);
                                    var OrgrEntity = _context.PRD_YearMonthCrustScheduleItem.First(m => m.ScheduleItemID == objCrustScheduleItem.ScheduleItemID);

                                    ScheduleItemID = objCrustScheduleItem.ScheduleItemID;
                                    ScheduleProductionNo = objCrustScheduleItem.ScheduleProductionNo;

                                    OrgrEntity.BuyerID = CurrEntity.BuyerID;
                                    OrgrEntity.BuyerOrderID = CurrEntity.BuyerOrderID;
                                    OrgrEntity.ArticleID = CurrEntity.ArticleID;
                                    OrgrEntity.ArticleChallanID = CurrEntity.ArticleChallanID;
                                    OrgrEntity.ArticleChallanNo = CurrEntity.ArticleChallanNo;
                                    OrgrEntity.ArticleNo = CurrEntity.ArticleNo;
                                    OrgrEntity.ItemTypeID = CurrEntity.ItemTypeID;
                                    OrgrEntity.LeatherStatusID = CurrEntity.LeatherStatusID;
                                    OrgrEntity.SchedulePcs = CurrEntity.SchedulePcs;
                                    OrgrEntity.ScheduleSide = CurrEntity.ScheduleSide;
                                    OrgrEntity.ScheduleArea = CurrEntity.ScheduleArea;
                                    OrgrEntity.ScheduleAreaUnit = CurrEntity.ScheduleAreaUnit;
                                    OrgrEntity.ScheduleWeight = CurrEntity.ScheduleWeight;
                                    OrgrEntity.ScheduleWeightUnit = CurrEntity.ScheduleWeightUnit;
                                    OrgrEntity.AvgSize = CurrEntity.AvgSize;
                                    OrgrEntity.AvgSizeUnit = CurrEntity.AvgSizeUnit;
                                    OrgrEntity.SelectionRange = CurrEntity.SelectionRange;
                                    OrgrEntity.SideDescription = CurrEntity.SideDescription;
                                    OrgrEntity.Thickness = CurrEntity.Thickness;
                                    OrgrEntity.ThicknessUnit = CurrEntity.ThicknessUnit;
                                    OrgrEntity.ThicknessAt = CurrEntity.ThicknessAt;
                                    OrgrEntity.ModifiedBy = userid;
                                    OrgrEntity.ModifiedOn = DateTime.Now;
                                }
                                if (model.PrdYearMonthCrustScheduleColorList != null)
                                {
                                    foreach (PrdYearMonthCrustScheduleColor objCrustScheduleColor in model.PrdYearMonthCrustScheduleColorList)
                                    {
                                        if (objCrustScheduleColor.SdulItemColorID == 0)
                                        {
                                            objCrustScheduleColor.ScheduleItemID = ScheduleItemID;
                                            objCrustScheduleColor.ProductionStatus = "Schedule";

                                            PRD_YearMonthCrustScheduleColor tblCrustScheduleColor = SetToModelObject(objCrustScheduleColor, userid);
                                            _context.PRD_YearMonthCrustScheduleColor.Add(tblCrustScheduleColor);
                                            _context.SaveChanges();

                                            SdulItemColorID = tblCrustScheduleColor.SdulItemColorID;
                                        }
                                        else
                                        {
                                            PRD_YearMonthCrustScheduleColor CurrEntity = SetToModelObject(objCrustScheduleColor, userid);
                                            var OrgrEntity = _context.PRD_YearMonthCrustScheduleColor.First(m => m.SdulItemColorID == objCrustScheduleColor.SdulItemColorID);

                                            SdulItemColorID = objCrustScheduleColor.SdulItemColorID;

                                            OrgrEntity.ColorID = CurrEntity.ColorID;
                                            OrgrEntity.ArticleColorNo = CurrEntity.ArticleColorNo;
                                            //OrgrEntity.GradeID = CurrEntity.GradeID;
                                            OrgrEntity.GradeRange = CurrEntity.GradeRange;
                                            OrgrEntity.ColorPCS = CurrEntity.ColorPCS;
                                            OrgrEntity.ColorSide = CurrEntity.ColorSide;
                                            OrgrEntity.ColorArea = CurrEntity.ColorArea;
                                            OrgrEntity.ColorWeight = CurrEntity.ColorWeight;
                                            OrgrEntity.AreaUnit = CurrEntity.AreaUnit;
                                            OrgrEntity.WeightUnit = CurrEntity.WeightUnit;
                                            //OrgrEntity.ProductionStatus = CurrEntity.ProductionStatus;
                                            OrgrEntity.Remarks = CurrEntity.Remarks;
                                            OrgrEntity.ModifiedBy = userid;
                                            OrgrEntity.ModifiedOn = DateTime.Now;
                                        }

                                        #region Save YearMonthCrustScheduleDrum Records

                                        if (model.PrdYearMonthCrustScheduleDrumList != null)
                                        {
                                            if (objCrustScheduleColor.ColorID == model.PrdYearMonthCrustScheduleDrumList[0].ColorID)
                                            {
                                                foreach (PrdYearMonthCrustScheduleDrum objCrustScheduleDrum in model.PrdYearMonthCrustScheduleDrumList)
                                                {
                                                    if (objCrustScheduleDrum.CrustSdulDrumID == 0)
                                                    {
                                                        objCrustScheduleDrum.SdulItemColorID = SdulItemColorID;
                                                        PRD_YearMonthCrustScheduleDrum tblCrustScheduleDrum = SetToModelObject(objCrustScheduleDrum, userid);
                                                        _context.PRD_YearMonthCrustScheduleDrum.Add(tblCrustScheduleDrum);
                                                    }
                                                    else
                                                    {
                                                        PRD_YearMonthCrustScheduleDrum CurEntity = SetToModelObject(objCrustScheduleDrum, userid);
                                                        var OrgEntity = _context.PRD_YearMonthCrustScheduleDrum.First(m => m.CrustSdulDrumID == objCrustScheduleDrum.CrustSdulDrumID);

                                                        OrgEntity.MachineID = CurEntity.MachineID;
                                                        OrgEntity.MachineNo = CurEntity.MachineNo;
                                                        OrgEntity.ColorID = CurEntity.ColorID;
                                                        //OrgEntity.GradeID = CurEntity.GradeID;
                                                        OrgEntity.GradeRange = CurEntity.GradeRange;
                                                        OrgEntity.DrumPcs = CurEntity.DrumPcs;
                                                        OrgEntity.DrumSide = CurEntity.DrumSide;
                                                        OrgEntity.DrumArea = CurEntity.DrumArea;
                                                        OrgEntity.DrumWeight = CurEntity.DrumWeight;
                                                        OrgEntity.AreaUnit = CurEntity.AreaUnit;
                                                        OrgEntity.WeightUnit = CurEntity.WeightUnit;
                                                        OrgEntity.Remarks = CurEntity.Remarks;
                                                        OrgEntity.ModifiedBy = userid;
                                                        OrgEntity.ModifiedOn = DateTime.Now;
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                }
                            }
                        }

                        #endregion

                        _context.SaveChanges();
                        tx.Complete();
                        YearMonID = model.YearMonID;
                        ScheduleNo = model.ScheduleNo;
                        ProductionNo = model.ProductionNo;
                        _vmMsg.Type = Enums.MessageType.Update;
                        _vmMsg.Msg = "Updated Successfully.";
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                _vmMsg.Type = Enums.MessageType.Error;
                _vmMsg.Msg = "Failed to Update.";

                if (ex.InnerException.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    _vmMsg.Type = Enums.MessageType.Error;
                    _vmMsg.Msg = "Schedule Year,Month Combination Data Already Exit.";
                }
            }
            return _vmMsg;
        }

        public PRD_YearMonth SetToModelObject(PrdCrustYearMonth model, int userid)
        {
            PRD_YearMonth Entity = new PRD_YearMonth();

            Entity.YearMonID = model.YearMonID;
            Entity.ScheduleYear = model.ScheduleYear;
            Entity.ScheduleMonth = model.ScheduleMonth;
            Entity.ScheduleFor = "CRP";//model.ScheduleFor;
            Entity.ProductionFloor = model.ProductionFloor;
            //Entity.ConcernStore = model.ConcernStore;
            Entity.Remarks = model.Remarks;
            Entity.RecordStatus = "NCF";
            Entity.SetOn = DateTime.Now;
            Entity.SetBy = userid;
            Entity.IPAddress = GetIPAddress.LocalIPAddress();

            return Entity;
        }

        public PRD_YearMonthSchedule SetToYearMonthScheduleModelObject(PrdCrustYearMonth model, int userid)
        {
            PRD_YearMonthSchedule Entity = new PRD_YearMonthSchedule();

            Entity.YearMonID = model.YearMonID;
            Entity.ScheduleNo = model.ScheduleNo;
            Entity.PrepareDate = DalCommon.SetDate(model.PrepareDate);
            //Entity.ScheduleStatus = model.ScheduleStatus;
            Entity.ProductionProcessID = model.ProductionProcessID;
            Entity.Remarks = model.Remarks;
            Entity.RecordStatus = "NCF";
            Entity.SetOn = DateTime.Now;
            Entity.SetBy = userid;
            Entity.IPAddress = GetIPAddress.LocalIPAddress();


            return Entity;
        }

        public PRD_YearMonthScheduleDate SetToScheduleDateModelObject(PrdCrustYearMonth model, int userid)
        {
            PRD_YearMonthScheduleDate Entity = new PRD_YearMonthScheduleDate();

            Entity.ScheduleDateID = model.ScheduleDateID;
            Entity.ProductionNo = model.ProductionNo;
            if (model.ScheduleID == 0)
                Entity.ScheduleID = null;
            else
                Entity.ScheduleID = model.ScheduleID;
            Entity.ScheduleStartDate = DalCommon.SetDate(model.ScheduleStartDate);
            Entity.ScheduleEndDate = DalCommon.SetDate(model.ScheduleEndDate);

            switch (model.ProductionStatus)
            {
                case "Schedule":
                    Entity.ProductionStatus = "SCH";
                    break;
                case "On Going":
                    Entity.ProductionStatus = "ONG";
                    break;
                case "Postponed":
                    Entity.ProductionStatus = "POS";
                    break;
                case "Completed":
                    Entity.ProductionStatus = "CMP";
                    break;
                default:
                    Entity.ProductionStatus = "SCH";
                    break;
            }
            Entity.RecordStatus = "NCF";
            //Entity.Remark = model.Remark;
            Entity.SetOn = DateTime.Now;
            Entity.SetBy = userid;
            Entity.IPAddress = GetIPAddress.LocalIPAddress();

            return Entity;
        }

        public PRD_YearMonthCrustScheduleItem SetToModelObject(PrdYearMonthCrustScheduleItem model, int userid)
        {
            PRD_YearMonthCrustScheduleItem Entity = new PRD_YearMonthCrustScheduleItem();

            Entity.ScheduleItemID = model.ScheduleItemID;
            Entity.ScheduleProductionNo = model.ScheduleProductionNo;
            Entity.ScheduleDateID = model.ScheduleDateID;
            Entity.ScheduleID = model.ScheduleID;
            Entity.YearMonID = model.YearMonID;
            Entity.BuyerID = model.BuyerID;
            Entity.BuyerOrderID = model.BuyerOrderID;
            Entity.ArticleID = model.ArticleID;
            Entity.ArticleChallanID = model.ArticleChallanID;
            Entity.ArticleChallanNo = model.ArticleChallanNo;
            Entity.ArticleNo = model.ArticleNo;
            Entity.ItemTypeID = model.ItemTypeID;
            Entity.LeatherStatusID = model.LeatherStatusID;
            Entity.LeatherTypeID = Convert.ToByte(_context.Sys_LeatherType.Where(m => m.LeatherTypeName == "Crust").FirstOrDefault().LeatherTypeID);
            Entity.SchedulePcs = model.SchedulePcs;
            Entity.ScheduleSide = model.ScheduleSide;
            Entity.ScheduleArea = model.ScheduleArea;
            Entity.ScheduleAreaUnit = model.ScheduleAreaUnit;
            Entity.ScheduleWeight = model.ScheduleWeight;
            Entity.ScheduleWeightUnit = model.ScheduleWeightUnit;
            Entity.AvgSize = model.AvgSize;
            Entity.AvgSizeUnit = model.AvgSizeUnit;
            Entity.SelectionRange = model.SelectionRange;
            Entity.SideDescription = model.SideDescription;
            Entity.Thickness = model.Thickness;
            Entity.ThicknessUnit = model.ThicknessUnit;
            Entity.ThicknessAt = model.ThicknessAt;
            Entity.SetOn = DateTime.Now;
            Entity.SetBy = userid;
            Entity.IPAddress = GetIPAddress.LocalIPAddress();

            return Entity;
        }

        public PRD_YearMonthCrustScheduleColor SetToModelObject(PrdYearMonthCrustScheduleColor model, int userid)
        {
            PRD_YearMonthCrustScheduleColor Entity = new PRD_YearMonthCrustScheduleColor();

            Entity.SdulItemColorID = model.SdulItemColorID;
            Entity.ScheduleItemID = model.ScheduleItemID;
            Entity.ColorID = model.ColorID;
            Entity.ArticleColorNo = model.ArticleColorNo;
            Entity.GradeID = model.GradeID;
            Entity.ColorPCS = model.ColorPCS;
            Entity.ColorSide = model.ColorSide;
            Entity.ColorArea = model.ColorArea;
            Entity.ColorWeight = model.ColorWeight;

            if (string.IsNullOrEmpty(model.AreaUnitName))
                Entity.AreaUnit = null;
            else
                Entity.AreaUnit = Convert.ToByte(_context.Sys_Unit.Where(m => m.UnitName == model.AreaUnitName).FirstOrDefault().UnitID);
            if (string.IsNullOrEmpty(model.WeightUnitName))
                Entity.WeightUnit = null;
            else
                Entity.WeightUnit = Convert.ToByte(_context.Sys_Unit.Where(m => m.UnitName == model.WeightUnitName).FirstOrDefault().UnitID);
            switch (model.ProductionStatus)
            {
                case "Schedule":
                    Entity.ProductionStatus = "SCH";
                    break;
                case "On Going":
                    Entity.ProductionStatus = "ONG";
                    break;
                case "Postponed":
                    Entity.ProductionStatus = "POS";
                    break;
                case "Completed":
                    Entity.ProductionStatus = "CMP";
                    break;
                default:
                    Entity.ProductionStatus = "SCH";
                    break;
            }
            Entity.Remarks = model.Remarks;
            Entity.SetOn = DateTime.Now;
            Entity.SetBy = userid;
            Entity.IPAddress = GetIPAddress.LocalIPAddress();

            return Entity;
        }

        public PRD_YearMonthCrustScheduleDrum SetToModelObject(PrdYearMonthCrustScheduleDrum model, int userid)
        {
            PRD_YearMonthCrustScheduleDrum Entity = new PRD_YearMonthCrustScheduleDrum();

            Entity.CrustSdulDrumID = model.CrustSdulDrumID;
            Entity.ScheduleDateID = model.ScheduleDateID;
            Entity.ScheduleItemID = model.ScheduleItemID;
            Entity.SdulItemColorID = model.SdulItemColorID;
            Entity.MachineID = model.MachineID;
            Entity.MachineNo = model.MachineNo;
            //Entity.ColorID = model.ColorID;
            //Entity.GradeID = model.GradeID;
            Entity.GradeRange = model.GradeRange;
            Entity.DrumPcs = model.DrumPcs;
            Entity.DrumSide = model.DrumSide;
            Entity.DrumArea = model.DrumArea;
            Entity.DrumWeight = model.DrumWeight;

            if (string.IsNullOrEmpty(model.AreaUnitName))
                Entity.AreaUnit = null;
            else
                Entity.AreaUnit = Convert.ToByte(_context.Sys_Unit.Where(m => m.UnitName == model.AreaUnitName).FirstOrDefault().UnitID);
            if (string.IsNullOrEmpty(model.WeightUnitName))
                Entity.WeightUnit = null;
            else
                Entity.WeightUnit = Convert.ToByte(_context.Sys_Unit.Where(m => m.UnitName == model.WeightUnitName).FirstOrDefault().UnitID);
            Entity.Remarks = model.Remarks;
            Entity.SetOn = DateTime.Now;
            Entity.SetBy = userid;
            Entity.IPAddress = GetIPAddress.LocalIPAddress();

            return Entity;
        }

        public long GetYearMonID()
        {
            return YearMonID;
        }
        public long GetYearMonScheduleID()
        {
            return ScheduleID;
        }
        public string GetYearMonScheduleNo()
        {
            return ScheduleNo;
        }
        public long GetYearMonScheduleDateID()
        {
            return ScheduleDateID;
        }
        public string GetYearMonScheduleDateNo()
        {
            return ProductionNo;
        }
        public long GetYearMonScheduleItemID()
        {
            return ScheduleItemID;
        }
        public string GetYearMonScheduleItemNo()
        {
            return ScheduleProductionNo;
        }

        public List<PrdYearMonthCrustScheduleColor> GetColorListForOrderItem(string _BuyerOrderItemID)
        {
            using (_context)
            {
                var Data = (from c in _context.SLS_BuyerOrderItemColor.AsEnumerable()
                            where c.BuyerOrderItemID.ToString() == _BuyerOrderItemID
                            join col in _context.Sys_Color on c.ColorID equals col.ColorID into Colors
                            from col in Colors.DefaultIfEmpty()
                            select new PrdYearMonthCrustScheduleColor
                            {
                                ColorID = c.ColorID,
                                ColorName = (col == null ? null : col.ColorName),
                                ColorArea = c.ColorQty,
                                GradeName = "Press F9",
                                AreaUnitName = "SFT",
                                WeightUnitName = "KG",
                                ProductionStatus = "Schedule"
                            }).ToList();

                return Data;
            }
        }

        public List<PrdCrustYearMonth> GetCrustYearMonthSchedule()
        {
            var query = @"select PRD_YearMonth.YearMonID,PRD_YearMonth.ScheduleYear,
                        PRD_YearMonth.ScheduleMonth,PRD_YearMonth.ScheduleFor,
                        PRD_YearMonth.ProductionFloor,PRD_YearMonthSchedule.ScheduleID,
                        PRD_YearMonthSchedule.ScheduleNo,
                        CONVERT(VARCHAR(12),PRD_YearMonthSchedule.PrepareDate,106)PrepareDate,
                        PRD_YearMonthSchedule.ProductionProcessID,Sys_ProductionProces.ProcessName,
						PRD_YearMonth.RecordStatus
                        from PRD_YearMonth
                        inner join PRD_YearMonthSchedule on PRD_YearMonth.YearMonID = PRD_YearMonthSchedule.YearMonID
                        inner join Sys_ProductionProces on Sys_ProductionProces.ProcessID = PRD_YearMonthSchedule.ProductionProcessID
                        where ScheduleFor ='CRP' order by PRD_YearMonth.YearMonID desc";
            var searchList = _context.Database.SqlQuery<PrdCrustYearMonth>(query).ToList();
            return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdCrustYearMonth>();
        }

        public PrdCrustYearMonth SetToBussinessObject(PrdCrustYearMonth Entity)
        {
            PrdCrustYearMonth Model = new PrdCrustYearMonth();

            Model.YearMonID = Entity.YearMonID;
            Model.ScheduleYear = Entity.ScheduleYear;
            Model.ScheduleMonth = Entity.ScheduleMonth;
            switch (Model.ScheduleMonth)
            {
                case "01":
                    Model.ScheduleMonthName = "January";
                    break;
                case "02":
                    Model.ScheduleMonthName = "February";
                    break;
                case "03":
                    Model.ScheduleMonthName = "March";
                    break;
                case "04":
                    Model.ScheduleMonthName = "April";
                    break;
                case "05":
                    Model.ScheduleMonthName = "May";
                    break;
                case "06":
                    Model.ScheduleMonthName = "June";
                    break;
                case "07":
                    Model.ScheduleMonthName = "July";
                    break;
                case "08":
                    Model.ScheduleMonthName = "August";
                    break;
                case "09":
                    Model.ScheduleMonthName = "September";
                    break;
                case "10":
                    Model.ScheduleMonthName = "October";
                    break;
                case "11":
                    Model.ScheduleMonthName = "November";
                    break;
                case "12":
                    Model.ScheduleMonthName = "December";
                    break;
                default:
                    Model.ScheduleMonthName = "";
                    break;
            }
            Model.ScheduleFor = Entity.ScheduleFor;
            switch (Model.ScheduleFor)
            {
                case "WBP":
                    Model.ScheduleForName = "Wet Blue Production";
                    break;
                case "WBR":
                    Model.ScheduleForName = "Wet Bule Requisition";
                    break;
                case "CRP":
                    Model.ScheduleForName = "Crust Production";
                    break;
                case "CRR":
                    Model.ScheduleForName = "Crust Requisition";
                    break;
                case "FNP":
                    Model.ScheduleForName = "Finished Production";
                    break;
                case "FNR":
                    Model.ScheduleForName = "Finished Requisition";
                    break;
                default:
                    Model.ScheduleForName = "";
                    break;
            }
            Model.ProductionFloor = Entity.ProductionFloor;
            //Model.ConcernStore = Entity.ProductionFloor;
            Model.ProductionFloorName = Model.ProductionFloor == null ? "" : _context.SYS_Store.Where(m => m.StoreID == Model.ProductionFloor).FirstOrDefault().StoreName;
            Model.RecordStatus = Entity.RecordStatus;
            Model.ScheduleID = Entity.ScheduleID;
            Model.ScheduleNo = Entity.ScheduleNo;
            Model.YearMonID = Convert.ToInt64(Entity.YearMonID);
            Model.PrepareDate = Convert.ToDateTime(Entity.PrepareDate).ToString("dd/MM/yyyy");
            Model.ProductionProcessID = Entity.ProductionProcessID;
            Model.ProcessName = Entity.ProcessName;
            Model.RecordStatus = Entity.RecordStatus;
            return Model;
        }

        public List<PrdCrustYearMonth> GetCrustYearMonth()
        {
            List<PRD_YearMonth> searchList = _context.PRD_YearMonth.Where(m => m.ScheduleFor == "CRP").OrderByDescending(m => m.YearMonID).ToList();
            return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdCrustYearMonth>();
        }

        public PrdCrustYearMonth SetToBussinessObject(PRD_YearMonth Entity)
        {
            PrdCrustYearMonth Model = new PrdCrustYearMonth();

            Model.YearMonID = Entity.YearMonID;
            Model.ScheduleYear = Entity.ScheduleYear;
            Model.ScheduleMonth = Entity.ScheduleMonth;
            switch (Model.ScheduleMonth)
            {
                case "01":
                    Model.ScheduleMonthName = "January";
                    break;
                case "02":
                    Model.ScheduleMonthName = "February";
                    break;
                case "03":
                    Model.ScheduleMonthName = "March";
                    break;
                case "04":
                    Model.ScheduleMonthName = "April";
                    break;
                case "05":
                    Model.ScheduleMonthName = "May";
                    break;
                case "06":
                    Model.ScheduleMonthName = "June";
                    break;
                case "07":
                    Model.ScheduleMonthName = "July";
                    break;
                case "08":
                    Model.ScheduleMonthName = "August";
                    break;
                case "09":
                    Model.ScheduleMonthName = "September";
                    break;
                case "10":
                    Model.ScheduleMonthName = "October";
                    break;
                case "11":
                    Model.ScheduleMonthName = "November";
                    break;
                case "12":
                    Model.ScheduleMonthName = "December";
                    break;
                default:
                    Model.ScheduleMonthName = "";
                    break;
            }
            Model.ScheduleFor = Entity.ScheduleFor;
            switch (Model.ScheduleFor)
            {
                case "WBP":
                    Model.ScheduleForName = "Wet Blue Production";
                    break;
                case "WBR":
                    Model.ScheduleForName = "Wet Bule Requisition";
                    break;
                case "CRP":
                    Model.ScheduleForName = "Crust Production";
                    break;
                case "CRR":
                    Model.ScheduleForName = "Crust Requisition";
                    break;
                case "FNP":
                    Model.ScheduleForName = "Finished Production";
                    break;
                case "FNR":
                    Model.ScheduleForName = "Finished Requisition";
                    break;
                default:
                    Model.ScheduleForName = "";
                    break;
            }
            Model.ProductionFloor = Entity.ProductionFloor;
            //Model.ConcernStore = Entity.ProductionFloor;
            Model.ProductionFloorName = Model.ProductionFloor == null ? "" : _context.SYS_Store.Where(m => m.StoreID == Model.ProductionFloor).FirstOrDefault().StoreName;
            Model.RecordStatus = Entity.RecordStatus;

            return Model;
        }

        public List<PrdCrustYearMonth> GetYearMonthScheduleList(string YearMonID)
        {
            if (!string.IsNullOrEmpty(YearMonID))
            {
                long? yearMonID = Convert.ToInt64(YearMonID);
                List<PRD_YearMonthSchedule> searchList = _context.PRD_YearMonthSchedule.Where(m => m.YearMonID == yearMonID).OrderByDescending(m => m.ScheduleID).ToList();
                return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdCrustYearMonth>();
            }
            return null;
        }

        public PrdCrustYearMonth SetToBussinessObject(PRD_YearMonthSchedule Entity)
        {
            PrdCrustYearMonth Model = new PrdCrustYearMonth();

            Model.ScheduleID = Entity.ScheduleID;
            Model.ScheduleNo = Entity.ScheduleNo;
            Model.YearMonID = Convert.ToInt64(Entity.YearMonID);
            Model.PrepareDate = Convert.ToDateTime(Entity.PrepareDate).ToString("dd/MM/yyyy");
            Model.ProductionProcessID = Entity.ProductionProcessID;
            Model.ProcessName = Entity.ProductionProcessID == null ? "" : _context.Sys_ProductionProces.Where(m => m.ProcessID == Entity.ProductionProcessID).FirstOrDefault().ProcessName;

            return Model;
        }

        public List<PrdCrustYearMonth> GetYearMonthScheduleDateInfo(string ScheduleID)
        {
            if (!string.IsNullOrEmpty(ScheduleID))
            {
                long? scheduleID = Convert.ToInt64(ScheduleID);
                List<PRD_YearMonthScheduleDate> searchList = _context.PRD_YearMonthScheduleDate.Where(m => m.ScheduleID == scheduleID).OrderByDescending(m => m.ScheduleDateID).ToList();
                return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdCrustYearMonth>();
            }
            return null;
        }

        public List<PrdYearMonthCrustScheduleItem> GetScheduleItemInfo(string ScheduleDateID)
        {
            if (!string.IsNullOrEmpty(ScheduleDateID))
            {
                long? scheduleDateID = Convert.ToInt64(ScheduleDateID);
                List<PRD_YearMonthCrustScheduleItem> searchList = _context.PRD_YearMonthCrustScheduleItem.Where(m => m.ScheduleDateID == scheduleDateID).OrderByDescending(m => m.ScheduleItemID).ToList();
                return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdYearMonthCrustScheduleItem>();
            }
            return null;
        }

        public PrdCrustYearMonth SetToBussinessObject(PRD_YearMonthScheduleDate Entity)
        {
            PrdCrustYearMonth Model = new PrdCrustYearMonth();

            Model.ScheduleDateID = Entity.ScheduleDateID;
            Model.ProductionNo = Entity.ProductionNo;
            Model.ScheduleStartDate = Convert.ToDateTime(Entity.ScheduleStartDate).ToString("dd/MM/yyyy");
            Model.ScheduleEndDate = Convert.ToDateTime(Entity.ScheduleEndDate).ToString("dd/MM/yyyy");
            Model.ProductionStatus = Entity.ProductionStatus;
            //Model.ProcessName = Entity.ProductionProcessID == null ? "" : _context.Sys_ProductionProces.Where(m => m.ProcessID == Entity.ProductionProcessID).FirstOrDefault().ProcessName;

            return Model;
        }

        public List<PrdYearMonthCrustScheduleItem> GetScheduleItemInfo()
        {
            List<PRD_YearMonthCrustScheduleItem> searchList = _context.PRD_YearMonthCrustScheduleItem.OrderByDescending(m => m.ScheduleItemID).ToList();
            return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdYearMonthCrustScheduleItem>();
        }

        public PrdYearMonthCrustScheduleItem SetToBussinessObject(PRD_YearMonthCrustScheduleItem Entity)
        {
            PrdYearMonthCrustScheduleItem Model = new PrdYearMonthCrustScheduleItem();

            Model.ScheduleItemID = Entity.ScheduleItemID;
            Model.ScheduleProductionNo = Entity.ScheduleProductionNo;
            Model.BuyerID = Entity.BuyerID;
            Model.BuyerName = Entity.BuyerID == null ? "" : _context.Sys_Buyer.Where(m => m.BuyerID == Entity.BuyerID).FirstOrDefault().BuyerName;
            Model.BuyerOrderID = Entity.BuyerOrderID;
            Model.BuyerOrderNo = Entity.BuyerOrderID == null ? "" : _context.SLS_BuyerOrder.Where(m => m.BuyerOrderID == Entity.BuyerOrderID).FirstOrDefault().BuyerOrderNo;
            Model.ArticleID = Entity.ArticleID;
            Model.ArticleName = Entity.ArticleID == null ? "" : _context.Sys_Article.Where(m => m.ArticleID == Entity.ArticleID).FirstOrDefault().ArticleName;
            Model.ArticleChallanID = Entity.ArticleChallanID;
            Model.ArticleChallanNo = Entity.ArticleChallanNo;
            Model.AvgSize = Entity.AvgSize;
            Model.AvgSizeUnit = Entity.AvgSizeUnit;
            Model.SelectionRange = Entity.SelectionRange;
            Model.SideDescription = Entity.SideDescription;
            Model.Thickness = Entity.Thickness;
            Model.ThicknessUnit = Entity.ThicknessUnit;
            //Model.ProcessName = Entity.ProductionProcessID == null ? "" : _context.Sys_ProductionProces.Where(m => m.ProcessID == Entity.ProductionProcessID).FirstOrDefault().ProcessName;

            Model.ThicknessAt = Entity.ThicknessAt;
            Model.ItemTypeID = Entity.ItemTypeID;
            Model.LeatherStatusID = Entity.LeatherStatusID;
            Model.LeatherTypeID = Entity.LeatherTypeID;

            Model.PrdYearMonthCrustScheduleColorList = YearMonthCrustScheduleColorList(Entity.ScheduleItemID.ToString());
            //if (Model.PrdYearMonthCrustScheduleColorList.Count > 0)
            //    Model.PrdYearMonthCrustScheduleDrumList = YearMonthCrustScheduleDrumList(Model.PrdYearMonthCrustScheduleColorList[0].SdulItemColorID.ToString());

            Model.LeatherStatusID = Entity.LeatherStatusID;
            Model.LeatherTypeID = Entity.LeatherTypeID;

            Model.SchedulePcs = Entity.SchedulePcs;
            Model.ScheduleSide = Entity.ScheduleSide;
            Model.ScheduleArea = Entity.ScheduleArea;
            Model.ScheduleAreaUnit = Entity.ScheduleAreaUnit;
            Model.ScheduleWeight = Entity.ScheduleWeight;
            Model.ScheduleWeightUnit = Entity.ScheduleWeightUnit;

            return Model;
        }

        public List<PrdYearMonthCrustScheduleColor> YearMonthCrustScheduleColorList(string ScheduleItemID)
        {
            long? scheduleItemID = Convert.ToInt64(ScheduleItemID);
            List<PRD_YearMonthCrustScheduleColor> searchList = _context.PRD_YearMonthCrustScheduleColor.Where(m => m.ScheduleItemID == scheduleItemID).OrderByDescending(m => m.SdulItemColorID).ToList();
            return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdYearMonthCrustScheduleColor>();
        }

        public PrdYearMonthCrustScheduleColor SetToBussinessObject(PRD_YearMonthCrustScheduleColor Entity)
        {
            PrdYearMonthCrustScheduleColor Model = new PrdYearMonthCrustScheduleColor();

            Model.SdulItemColorID = Entity.SdulItemColorID;
            Model.ScheduleItemID = Entity.ScheduleItemID;
            Model.ColorID = Entity.ColorID;
            Model.ColorName = Entity.ColorID == null ? "" : _context.Sys_Color.Where(m => m.ColorID == Entity.ColorID).FirstOrDefault().ColorName;
            Model.ArticleColorNo = Entity.ArticleColorNo;
            //Model.GradeID = Entity.GradeID;
            //Model.GradeName = Entity.GradeID == null ? "" : _context.Sys_Grade.Where(m => m.GradeID == Entity.GradeID).FirstOrDefault().GradeName;
            Model.ColorPCS = Entity.ColorPCS;
            Model.ColorSide = Entity.ColorSide;
            Model.ColorArea = Entity.ColorArea;
            Model.AreaUnit = Entity.AreaUnit;
            Model.ColorWeight = Entity.ColorWeight;
            Model.WeightUnit = Entity.WeightUnit;
            Model.AreaUnitName = Entity.AreaUnit == null ? "SFT" : _context.Sys_Unit.Where(m => m.UnitID == Entity.AreaUnit).FirstOrDefault().UnitName;
            Model.WeightUnitName = Entity.WeightUnit == null ? "KG" : _context.Sys_Unit.Where(m => m.UnitID == Entity.WeightUnit).FirstOrDefault().UnitName;
            //Model.ProductionStatus = Entity.ProductionStatus;
            switch (Entity.ProductionStatus)
            {
                case "SCH":
                    Model.ProductionStatus = "Schedule";
                    break;
                case "ONG":
                    Model.ProductionStatus = "On Going";
                    break;
                case "POS":
                    Model.ProductionStatus = "Postponed";
                    break;
                case "CMP":
                    Model.ProductionStatus = "Completed";
                    break;
                default:
                    Model.ProductionStatus = "";
                    break;
            }
            Model.Remarks = Entity.Remarks;

            return Model;
        }

        public List<PrdYearMonthCrustScheduleDrum> YearMonthCrustScheduleDrumList(string SdulItemColorID, string ProductionFloor, string BuyerID, string BuyerOrderID, string ArticleID, string ItemTypeID, string LeatherStatusID)
        {

            long? sdulItemColorID = Convert.ToInt64(SdulItemColorID);
            List<PRD_YearMonthCrustScheduleDrum> searchList = _context.PRD_YearMonthCrustScheduleDrum.Where(m => m.SdulItemColorID == sdulItemColorID).OrderByDescending(m => m.SdulItemColorID).ToList();
            return searchList.Select(c => SetToBussinessObject(c, ProductionFloor, BuyerID, BuyerOrderID, ArticleID, ItemTypeID, LeatherStatusID)).ToList<PrdYearMonthCrustScheduleDrum>();

        }

        public ValidationMsg ConfirmedCrustProductionSchedule(PrdCrustYearMonth model, int userid)
        {
            _vmMsg = new ValidationMsg();
            try
            {
                using (var tx = new TransactionScope())
                {
                    using (_context)
                    {
                        var originalEntityYearMonth = _context.PRD_YearMonth.First(m => m.YearMonID == model.YearMonID);
                        originalEntityYearMonth.RecordStatus = "CNF";
                        originalEntityYearMonth.ModifiedBy = userid;
                        originalEntityYearMonth.ModifiedOn = DateTime.Now;

                        var originalEntityYearMonthSchedule = _context.PRD_YearMonthSchedule.First(m => m.ScheduleNo == model.ScheduleNo);
                        originalEntityYearMonthSchedule.RecordStatus = "CNF";
                        originalEntityYearMonthSchedule.ModifiedBy = userid;
                        originalEntityYearMonthSchedule.ModifiedOn = DateTime.Now;

                        //if (model.PrdYearMonthCrustScheduleColorList.Count > 0)
                        //{
                        //    foreach (PrdYearMonthScheduleDate objPrdYearMonthScheduleDate in model.PrdYearMonthCrustScheduleColorList)
                        //    {
                        //        var originalEntityYearMonthScheduleDate = _context.PRD_YearMonthScheduleDate.First(m => m.ScheduleDateID == objPrdYearMonthScheduleDate.ScheduleDateID);
                        //        originalEntityYearMonthScheduleDate.RecordStatus = "CNF";
                        //        originalEntityYearMonthScheduleDate.ModifiedBy = userid;
                        //        originalEntityYearMonthScheduleDate.ModifiedOn = DateTime.Now;
                        //    }
                        //    if (model.PrdYearMonthCrustScheduleDrumList.Count > 0)
                        //    {
                        //        foreach (PrdYearMonthSchedulePurchase objScheduleDrum in model.PrdYearMonthCrustScheduleDrumList)
                        //        {
                        //            var originalEntityYearMonthSchedulePurchase = _context.PRD_YearMonthCrustScheduleDrum.First(m => m.SchedulePurchaseID == objScheduleDrum.SchedulePurchaseID);
                        //            originalEntityYearMonthSchedulePurchase.RecordStatus = "CNF";
                        //            originalEntityYearMonthSchedulePurchase.ModifiedBy = userid;
                        //            originalEntityYearMonthSchedulePurchase.ModifiedOn = DateTime.Now;
                        //        }
                        //    }
                        //}

                        _context.SaveChanges();

                        tx.Complete();
                        _vmMsg.Type = Enums.MessageType.Success;
                        _vmMsg.Msg = "Confirmed Successfully.";
                    }
                }
            }
            catch
            {
                _vmMsg.Type = Enums.MessageType.Error;
                _vmMsg.Msg = "Failed to Confirmed.";
            }
            return _vmMsg;
        }

        public ValidationMsg CheckedCrustProductionSchedule(PrdCrustYearMonth model, int userid)
        {
            _vmMsg = new ValidationMsg();
            try
            {
                using (var tx = new TransactionScope())
                {
                    using (_context)
                    {
                        var originalEntityYearMonth = _context.PRD_YearMonth.First(m => m.YearMonID == model.YearMonID);
                        originalEntityYearMonth.RecordStatus = "CHK";
                        originalEntityYearMonth.ModifiedBy = userid;
                        originalEntityYearMonth.ModifiedOn = DateTime.Now;

                        var originalEntityYearMonthSchedule = _context.PRD_YearMonthSchedule.First(m => m.ScheduleNo == model.ScheduleNo);
                        originalEntityYearMonthSchedule.RecordStatus = "CHK";
                        originalEntityYearMonthSchedule.ModifiedBy = userid;
                        originalEntityYearMonthSchedule.ModifiedOn = DateTime.Now;

                        //if (model.PrdYearMonthCrustScheduleColorList.Count > 0)
                        //{
                        //    foreach (PrdYearMonthScheduleDate objPrdYearMonthScheduleDate in model.PrdYearMonthCrustScheduleColorList)
                        //    {
                        //        var originalEntityYearMonthScheduleDate = _context.PRD_YearMonthScheduleDate.First(m => m.ScheduleDateID == objPrdYearMonthScheduleDate.ScheduleDateID);
                        //        originalEntityYearMonthScheduleDate.RecordStatus = "CHK";
                        //        originalEntityYearMonthScheduleDate.ModifiedBy = userid;
                        //        originalEntityYearMonthScheduleDate.ModifiedOn = DateTime.Now;
                        //    }
                        //    if (model.PrdYearMonthCrustScheduleDrumList.Count > 0)
                        //    {
                        //        foreach (PrdYearMonthSchedulePurchase objScheduleDrum in model.PrdYearMonthCrustScheduleDrumList)
                        //        {
                        //            var originalEntityYearMonthSchedulePurchase = _context.PRD_YearMonthCrustScheduleDrum.First(m => m.SchedulePurchaseID == objScheduleDrum.SchedulePurchaseID);
                        //            originalEntityYearMonthSchedulePurchase.RecordStatus = "CHK";
                        //            originalEntityYearMonthSchedulePurchase.ModifiedBy = userid;
                        //            originalEntityYearMonthSchedulePurchase.ModifiedOn = DateTime.Now;
                        //        }
                        //    }
                        //}

                        _context.SaveChanges();

                        tx.Complete();
                        _vmMsg.Type = Enums.MessageType.Success;
                        _vmMsg.Msg = "Checked Successfully.";
                    }
                }
            }
            catch
            {
                _vmMsg.Type = Enums.MessageType.Error;
                _vmMsg.Msg = "Failed to Checked.";
            }
            return _vmMsg;
        }

        public PrdYearMonthCrustScheduleDrum SetToBussinessObject(PRD_YearMonthCrustScheduleDrum Entity, string ProductionFloor, string BuyerID, string BuyerOrderID, string ArticleID, string ItemTypeID, string LeatherStatusID)
        {
            PrdYearMonthCrustScheduleDrum Model = new PrdYearMonthCrustScheduleDrum();

            Model.CrustSdulDrumID = Entity.CrustSdulDrumID;
            Model.SdulItemColorID = Entity.SdulItemColorID;
            Model.MachineID = Entity.MachineID;
            Model.MachineNo = Entity.MachineNo;
            Model.ColorID = Entity.ColorID;
            Model.ColorName = Entity.ColorID == null ? "" : _context.Sys_Color.Where(m => m.ColorID == Entity.ColorID).FirstOrDefault().ColorName;
            //Model.GradeID = Entity.GradeID;
            //Model.GradeName = Entity.GradeID == null ? "" : _context.Sys_Grade.Where(m => m.GradeID == Entity.GradeID).FirstOrDefault().GradeName;
            Model.GradeRange = Entity.GradeRange;
            Model.DrumPcs = Entity.DrumPcs;
            Model.DrumSide = Entity.DrumSide;
            Model.DrumArea = Entity.DrumArea;
            Model.AreaUnit = Entity.AreaUnit;
            Model.DrumWeight = Entity.DrumWeight;
            Model.WeightUnit = Entity.WeightUnit;
            Model.AreaUnitName = Entity.AreaUnit == null ? "" : _context.Sys_Unit.Where(m => m.UnitID == Entity.AreaUnit).FirstOrDefault().UnitName;
            Model.WeightUnitName = Entity.WeightUnit == null ? "" : _context.Sys_Unit.Where(m => m.UnitID == Entity.WeightUnit).FirstOrDefault().UnitName;
            Model.Remarks = Entity.Remarks;

            byte? storeId = null;
            if (!string.IsNullOrEmpty(ProductionFloor))
                storeId = Convert.ToByte(ProductionFloor);
            int? buyerID = null;
            if (!string.IsNullOrEmpty(BuyerID))
                buyerID = Convert.ToInt32(BuyerID);
            long? buyerOrderID = null;
            if (!string.IsNullOrEmpty(BuyerOrderID))
                buyerOrderID = Convert.ToInt64(BuyerOrderID);
            byte? itemTypeID = null;
            if (!string.IsNullOrEmpty(ItemTypeID))
                itemTypeID = Convert.ToByte(ItemTypeID);
            byte? leatherStatusID = null;
            if (!string.IsNullOrEmpty(LeatherStatusID))
                leatherStatusID = Convert.ToByte(LeatherStatusID);
            int? articleID = null;
            if (!string.IsNullOrEmpty(ArticleID))
                articleID = Convert.ToInt32(ArticleID);

            var ProductionPcs = _context.PRD_CrustLeatherProductionStock.Where(ma => ma.StoreID == storeId
               && ma.BuyerID == buyerID
               && ma.BuyerOrderID == buyerOrderID
               && ma.ItemTypeID == itemTypeID
               && ma.LeatherStatusID == leatherStatusID
               && ma.ArticleID == articleID
               && ma.GradeID == Entity.GradeID
               && ma.QCStatus == "REP").OrderByDescending(m => m.TransectionID).FirstOrDefault();
            if (ProductionPcs == null)
                Model.ProductionPcs = 0;
            else
                Model.ProductionPcs = _context.PRD_CrustLeatherProductionStock.Where(ma => ma.StoreID == storeId
               && ma.BuyerID == buyerID
               && ma.BuyerOrderID == buyerOrderID
               && ma.ItemTypeID == itemTypeID
               && ma.LeatherStatusID == leatherStatusID
               && ma.ArticleID == articleID
               && ma.GradeID == Entity.GradeID
               && ma.QCStatus == "REP").OrderByDescending(m => m.TransectionID).FirstOrDefault().ClosingProductionPcs;

            var ProductionSide = _context.PRD_CrustLeatherProductionStock.Where(ma => ma.StoreID == storeId
                && ma.BuyerID == buyerID
                && ma.BuyerOrderID == buyerOrderID
                && ma.ItemTypeID == itemTypeID
                && ma.LeatherStatusID == leatherStatusID
                && ma.ArticleID == articleID
                && ma.GradeID == Entity.GradeID
                && ma.QCStatus == "REP").OrderByDescending(m => m.TransectionID).FirstOrDefault();
            if (ProductionSide == null)
                Model.ProductionSide = 0;
            else
                Model.ProductionSide = _context.PRD_CrustLeatherProductionStock.Where(ma => ma.StoreID == storeId
                    && ma.BuyerID == buyerID
                    && ma.BuyerOrderID == buyerOrderID
                    && ma.ItemTypeID == itemTypeID
                    && ma.LeatherStatusID == leatherStatusID
                    && ma.ArticleID == articleID
                    && ma.GradeID == Entity.GradeID
                    && ma.QCStatus == "REP").OrderByDescending(m => m.TransectionID).FirstOrDefault().ClosingProductionSide;
            var ProductionArea = _context.PRD_CrustLeatherProductionStock.Where(ma => ma.StoreID == storeId
                && ma.BuyerID == buyerID
                && ma.BuyerOrderID == buyerOrderID
                && ma.ItemTypeID == itemTypeID
                && ma.LeatherStatusID == leatherStatusID
                && ma.ArticleID == articleID
                && ma.GradeID == Entity.GradeID
                && ma.QCStatus == "REP").OrderByDescending(m => m.TransectionID).FirstOrDefault();
            if (ProductionArea == null)
                Model.ProductionArea = 0;
            else
                Model.ProductionArea = _context.PRD_CrustLeatherProductionStock.Where(ma => ma.StoreID == storeId
                    && ma.BuyerID == buyerID
                    && ma.BuyerOrderID == buyerOrderID
                    && ma.ItemTypeID == itemTypeID
                    && ma.LeatherStatusID == leatherStatusID
                    && ma.ArticleID == articleID
                    && ma.GradeID == Entity.GradeID
                    && ma.QCStatus == "REP").OrderByDescending(m => m.TransectionID).FirstOrDefault().ClosingProductionArea;

            return Model;
        }

        public ValidationMsg ExecuteCrustProductionSchedule(PrdCrustYearMonth model, int userid)
        {
            _vmMsg = new ValidationMsg();
            try
            {
                using (var tx = new TransactionScope())
                {
                    using (_context)
                    {
                        if (model.PrdYearMonthCrustScheduleColorList[0].SdulItemColorID != 0)
                        {
                            long sdulItemColorID = Convert.ToInt64(model.PrdYearMonthCrustScheduleColorList[0].SdulItemColorID);
                            var productionStatus = _context.PRD_YearMonthCrustScheduleColor.Where(m => m.SdulItemColorID == sdulItemColorID).FirstOrDefault().ProductionStatus;
                            var processEffect = _context.Sys_ProductionProces.Where(m => m.ProcessID == model.ProductionProcessID).FirstOrDefault().ProcessEffect;

                            if ((productionStatus == "SCH") && (processEffect == "CL"))
                            {
                                if (model.PrdYearMonthCrustScheduleDrumList != null)
                                {
                                    foreach (PrdYearMonthCrustScheduleDrum objScheduleDrum in model.PrdYearMonthCrustScheduleDrumList)
                                    {
                                        int? colorid = model.PrdYearMonthCrustScheduleColorList[0].ColorID;
                                        int? articleColorNo = model.PrdYearMonthCrustScheduleColorList[0].ArticleColorNo;
                                        //long? articleChallanID = Convert.ToInt64(model.ArticleChallanID);

                                        #region Schedule Running for Crust Production

                                        var CrustProductionStock = (from i in _context.PRD_CrustLeatherProductionStock
                                                                    where i.StoreID == model.ProductionFloor
                                                                    && i.BuyerID == model.BuyerID
                                                                    && i.BuyerOrderID == model.BuyerOrderID
                                                                    && i.ArticleID == model.ArticleID
                                                                    && i.ItemTypeID == model.ItemTypeID
                                                                    && i.LeatherStatusID == model.LeatherStatusID
                                                                    && i.ArticleChallanID == model.ArticleChallanID
                                                                    && i.QCStatus == "REP"
                                                                    select i).Any();

                                        if (CrustProductionStock)
                                        {
                                            var LastItemInfo = (from i in _context.PRD_CrustLeatherProductionStock
                                                                where i.StoreID == model.ProductionFloor
                                                                && i.BuyerID == model.BuyerID
                                                                && i.BuyerOrderID == model.BuyerOrderID
                                                                && i.ArticleID == model.ArticleID
                                                                && i.ItemTypeID == model.ItemTypeID
                                                                && i.LeatherStatusID == model.LeatherStatusID
                                                                && i.ArticleChallanID == model.ArticleChallanID
                                                                && i.QCStatus == "REP"
                                                                orderby i.TransectionID descending
                                                                select i).FirstOrDefault();
                                            decimal? schedulePcs = objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs;
                                            decimal? scheduleSide = objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide;
                                            decimal? scheduleArea = objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea;

                                            if ((LastItemInfo.ClosingProductionPcs >= schedulePcs) && (LastItemInfo.ClosingProductionSide >= scheduleSide) && (LastItemInfo.ClosingProductionArea >= scheduleArea))
                                            {
                                                #region Ready to Production

                                                var objREPStockItem = new PRD_CrustLeatherProductionStock();

                                                if (model.BuyerID == null)
                                                    objREPStockItem.BuyerID = null;
                                                else
                                                    objREPStockItem.BuyerID = Convert.ToInt32(model.BuyerID);
                                                if (model.BuyerOrderID == null)
                                                    objREPStockItem.BuyerOrderID = null;
                                                else
                                                    objREPStockItem.BuyerOrderID = Convert.ToInt32(model.BuyerOrderID);
                                                objREPStockItem.LeatherTypeID = Convert.ToByte(_context.Sys_LeatherType.Where(m => m.LeatherTypeName == "Crust").FirstOrDefault().LeatherTypeID);
                                                objREPStockItem.StoreID = Convert.ToByte(model.ProductionFloor);
                                                objREPStockItem.ItemTypeID = Convert.ToByte(model.ItemTypeID);
                                                objREPStockItem.LeatherStatusID = Convert.ToByte(model.LeatherStatusID);
                                                objREPStockItem.ArticleID = Convert.ToInt32(model.ArticleID);

                                                objREPStockItem.ArticleChallanID = model.ArticleChallanID;
                                                objREPStockItem.ArticleChallanNo = model.ArticleChallanNo;
                                                objREPStockItem.GradeRange = objScheduleDrum.GradeRange;

                                                if (string.IsNullOrEmpty(objScheduleDrum.AreaUnitName))
                                                    objREPStockItem.AreaUnit = null;
                                                else
                                                    objREPStockItem.AreaUnit = _context.Sys_Unit.Where(m => m.UnitName == objScheduleDrum.AreaUnitName).FirstOrDefault().UnitID;

                                                objREPStockItem.QCStatus = "REP";// Ready to Production

                                                objREPStockItem.OpeningPcs = LastItemInfo.ClosingProductionPcs;
                                                objREPStockItem.ReceivePcs = 0;
                                                objREPStockItem.IssuePcs = (objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs);
                                                objREPStockItem.ClosingProductionPcs = LastItemInfo.ClosingProductionPcs - (objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs);

                                                objREPStockItem.OpeningSide = LastItemInfo.ClosingProductionSide;
                                                objREPStockItem.ReceiveSide = 0;
                                                objREPStockItem.IssueSide = (objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide);
                                                objREPStockItem.ClosingProductionSide = LastItemInfo.ClosingProductionSide - (objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide);

                                                objREPStockItem.OpeningArea = LastItemInfo.ClosingProductionArea;
                                                objREPStockItem.ReceiveArea = 0;
                                                objREPStockItem.IssueArea = (objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea);
                                                objREPStockItem.ClosingProductionArea = LastItemInfo.ClosingProductionArea - (objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea);

                                                _context.PRD_CrustLeatherProductionStock.Add(objREPStockItem);
                                                _context.SaveChanges();

                                                #endregion

                                                #region On Production

                                                var CheckItemStock = (from i in _context.PRD_CrustLeatherProductionStock
                                                                      where i.StoreID == model.ProductionFloor
                                                                          && i.BuyerID == model.BuyerID
                                                                          && i.BuyerOrderID == model.BuyerOrderID
                                                                          && i.ArticleID == model.ArticleID
                                                                          && i.ItemTypeID == model.ItemTypeID
                                                                          && i.LeatherStatusID == model.LeatherStatusID
                                                                          && i.ColorID == colorid
                                                                          && i.ArticleChallanID == model.ArticleChallanID
                                                                          //&& i.GradeID == objScheduleDrum.GradeID
                                                                          //&& i.ArticleChallanNo == model.ArticleChallanNo
                                                                          && i.QCStatus == "ONP"
                                                                      select i).Any();
                                                if (!CheckItemStock)
                                                {
                                                    PRD_CrustLeatherProductionStock objStockItem = new PRD_CrustLeatherProductionStock();

                                                    objStockItem.StoreID = Convert.ToByte(model.ProductionFloor);
                                                    if (model.BuyerID == null)
                                                        objStockItem.BuyerID = null;
                                                    else
                                                        objStockItem.BuyerID = Convert.ToInt32(model.BuyerID);
                                                    if (model.BuyerOrderID == null)
                                                        objStockItem.BuyerOrderID = null;
                                                    else
                                                        objStockItem.BuyerOrderID = Convert.ToInt32(model.BuyerOrderID);
                                                    objStockItem.ArticleID = Convert.ToInt32(model.ArticleID);
                                                    objStockItem.ItemTypeID = Convert.ToByte(model.ItemTypeID);
                                                    objStockItem.LeatherTypeID = Convert.ToByte(_context.Sys_LeatherType.Where(m => m.LeatherTypeName == "Crust").FirstOrDefault().LeatherTypeID);
                                                    objStockItem.LeatherStatusID = Convert.ToByte(model.LeatherStatusID);
                                                    objStockItem.ColorID = Convert.ToInt32(colorid);

                                                    objStockItem.ArticleColorNo = Convert.ToInt32(articleColorNo);
                                                    objStockItem.ArticleChallanID = model.ArticleChallanID;
                                                    objStockItem.ArticleChallanNo = model.ArticleChallanNo;
                                                    objStockItem.GradeRange = objScheduleDrum.GradeRange;

                                                    if (string.IsNullOrEmpty(objScheduleDrum.AreaUnitName))
                                                        objStockItem.AreaUnit = null;
                                                    else
                                                        objStockItem.AreaUnit = Convert.ToByte(_context.Sys_Unit.Where(m => m.UnitName == objScheduleDrum.AreaUnitName).FirstOrDefault().UnitID);

                                                    objStockItem.QCStatus = "ONP";// Production Completed

                                                    objStockItem.OpeningPcs = 0;
                                                    objStockItem.ReceivePcs = objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs;
                                                    objStockItem.IssuePcs = 0;
                                                    objStockItem.ClosingProductionPcs = objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs;

                                                    objStockItem.OpeningSide = 0;
                                                    objStockItem.ReceiveSide = objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide;
                                                    objStockItem.IssueSide = 0;
                                                    objStockItem.ClosingProductionSide = objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide;

                                                    objStockItem.OpeningArea = 0;
                                                    objStockItem.ReceiveArea = objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea;
                                                    objStockItem.IssueArea = 0;
                                                    objStockItem.ClosingProductionArea = objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea;

                                                    _context.PRD_CrustLeatherProductionStock.Add(objStockItem);
                                                    _context.SaveChanges();
                                                }
                                                else
                                                {
                                                    var LastItemInfo1 = (from i in _context.PRD_CrustLeatherProductionStock
                                                                         where i.StoreID == model.ProductionFloor
                                                                         && i.BuyerID == model.BuyerID
                                                                         && i.BuyerOrderID == model.BuyerOrderID
                                                                         && i.ArticleID == model.ArticleID
                                                                         && i.ItemTypeID == model.ItemTypeID
                                                                         && i.LeatherStatusID == model.LeatherStatusID
                                                                         && i.ColorID == colorid
                                                                         && i.ArticleChallanID == model.ArticleChallanID
                                                                             //&& i.GradeID == objScheduleDrum.GradeID
                                                                             //&& i.ArticleChallanNo == model.ArticleChallanNo
                                                                         && i.QCStatus == "ONP"
                                                                         orderby i.TransectionID descending
                                                                         select i).FirstOrDefault();

                                                    PRD_CrustLeatherProductionStock objStockItem = new PRD_CrustLeatherProductionStock();

                                                    if (model.BuyerID == null)
                                                        objStockItem.BuyerID = null;
                                                    else
                                                        objStockItem.BuyerID = Convert.ToInt32(model.BuyerID);
                                                    if (model.BuyerOrderID == null)
                                                        objStockItem.BuyerOrderID = null;
                                                    else
                                                        objStockItem.BuyerOrderID = Convert.ToInt32(model.BuyerOrderID);
                                                    objStockItem.StoreID = Convert.ToByte(model.ProductionFloor);
                                                    objStockItem.ArticleID = Convert.ToInt32(model.ArticleID);
                                                    objStockItem.ItemTypeID = Convert.ToByte(model.ItemTypeID);
                                                    objStockItem.LeatherTypeID = Convert.ToByte(_context.Sys_LeatherType.Where(m => m.LeatherTypeName == "Crust").FirstOrDefault().LeatherTypeID);
                                                    objStockItem.LeatherStatusID = Convert.ToByte(model.LeatherStatusID);
                                                    objStockItem.ColorID = Convert.ToInt32(colorid);

                                                    objStockItem.ArticleColorNo = Convert.ToInt32(articleColorNo);
                                                    objStockItem.ArticleChallanID = model.ArticleChallanID;
                                                    objStockItem.ArticleChallanNo = model.ArticleChallanNo;
                                                    objStockItem.GradeRange = objScheduleDrum.GradeRange;

                                                    objStockItem.QCStatus = "ONP";// Production Completed

                                                    objStockItem.OpeningPcs = LastItemInfo1.ClosingProductionPcs;
                                                    objStockItem.ReceivePcs = objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs;
                                                    objStockItem.IssuePcs = 0;
                                                    objStockItem.ClosingProductionPcs = LastItemInfo1.ClosingProductionPcs + (objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs);

                                                    objStockItem.OpeningSide = LastItemInfo1.ClosingProductionSide;
                                                    objStockItem.ReceiveSide = objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide;
                                                    objStockItem.IssueSide = 0;
                                                    objStockItem.ClosingProductionSide = LastItemInfo1.ClosingProductionSide + (objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide);

                                                    objStockItem.OpeningArea = LastItemInfo1.ClosingProductionArea;
                                                    objStockItem.ReceiveArea = objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea;
                                                    objStockItem.IssueArea = 0;
                                                    objStockItem.ClosingProductionArea = LastItemInfo1.ClosingProductionArea + (objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea);

                                                    _context.PRD_CrustLeatherProductionStock.Add(objStockItem);
                                                    _context.SaveChanges();
                                                }

                                                #endregion
                                            }
                                            else
                                            {
                                                stockError = 2;
                                            }
                                        }
                                        else
                                        {
                                            stockError = 1;
                                        }

                                        #endregion
                                    }
                                }
                            }
                            switch (model.PrdYearMonthCrustScheduleColorList[0].ProductionStatus)
                            {
                                case "Schedule":
                                case "On Going":
                                    {
                                        var OrgrEntity = _context.PRD_YearMonthCrustScheduleColor.First(m => m.SdulItemColorID == sdulItemColorID);

                                        OrgrEntity.ProductionStatus = "ONG";
                                        OrgrEntity.ModifiedBy = userid;
                                        OrgrEntity.ModifiedOn = DateTime.Now;
                                        _context.SaveChanges();
                                    }
                                    break;
                                case "Postponed":
                                    {
                                        if ((productionStatus == "SCH") || (productionStatus == "ONG"))
                                        {
                                            var OrgrEntity = _context.PRD_YearMonthCrustScheduleColor.First(m => m.SdulItemColorID == sdulItemColorID);

                                            OrgrEntity.ProductionStatus = "POS ";
                                            OrgrEntity.ModifiedBy = userid;
                                            OrgrEntity.ModifiedOn = DateTime.Now;
                                            _context.SaveChanges();
                                        }
                                    }
                                    break;
                                case "Completed":
                                    {
                                        if ((productionStatus == "POS") || (productionStatus == "ONG"))
                                        {
                                            var OrgrEntity = _context.PRD_YearMonthCrustScheduleColor.First(m => m.SdulItemColorID == sdulItemColorID);

                                            OrgrEntity.ProductionStatus = "CMP";
                                            //OrgrEntity.RecordStatus = "CNF";
                                            OrgrEntity.ModifiedBy = userid;
                                            OrgrEntity.ModifiedOn = DateTime.Now;
                                            if (model.PrdYearMonthCrustScheduleDrumList != null)
                                            {
                                                foreach (PrdYearMonthCrustScheduleDrum objScheduleDrum in model.PrdYearMonthCrustScheduleDrumList)
                                                {
                                                    int? colorid = model.PrdYearMonthCrustScheduleColorList[0].ColorID;

                                                    #region Update Stock

                                                    var CheckItemStock = (from i in _context.PRD_CrustLeatherProductionStock
                                                                          where i.StoreID == model.ProductionFloor
                                                                              && i.BuyerID == model.BuyerID
                                                                              && i.BuyerOrderID == model.BuyerOrderID
                                                                              && i.ArticleID == model.ArticleID
                                                                              && i.ItemTypeID == model.ItemTypeID
                                                                              && i.LeatherStatusID == model.LeatherStatusID
                                                                              && i.ColorID == colorid
                                                                              && i.ArticleChallanID == model.ArticleChallanID
                                                                              //&& i.ArticleChallanNo == model.ArticleChallanNo
                                                                              //&& i.ArticleColorNo == model.PrdYearMonthCrustScheduleColorList[0].ArticleColorNo
                                                                              //i.GradeID == objScheduleDrum.GradeID
                                                                              && i.QCStatus == "PCM"
                                                                          select i).Any();
                                                    if (!CheckItemStock)
                                                    {
                                                        PRD_CrustLeatherProductionStock objStockItem = new PRD_CrustLeatherProductionStock();

                                                        objStockItem.StoreID = Convert.ToByte(model.ProductionFloor);
                                                        if (model.BuyerID == null)
                                                            objStockItem.BuyerID = null;
                                                        else
                                                            objStockItem.BuyerID = Convert.ToInt32(model.BuyerID);
                                                        if (model.BuyerOrderID == null)
                                                            objStockItem.BuyerOrderID = null;
                                                        else
                                                            objStockItem.BuyerOrderID = Convert.ToInt32(model.BuyerOrderID);
                                                        objStockItem.ArticleID = Convert.ToInt32(model.ArticleID);
                                                        objStockItem.ItemTypeID = Convert.ToByte(model.ItemTypeID);
                                                        objStockItem.LeatherTypeID = Convert.ToByte(_context.Sys_LeatherType.Where(m => m.LeatherTypeName == "Crust").FirstOrDefault().LeatherTypeID);
                                                        objStockItem.LeatherStatusID = Convert.ToByte(model.LeatherStatusID);
                                                        objStockItem.ColorID = Convert.ToInt32(colorid);

                                                        objStockItem.ArticleChallanID = model.ArticleChallanID;
                                                        objStockItem.ArticleChallanNo = model.ArticleChallanNo;
                                                        objStockItem.ArticleColorNo = model.PrdYearMonthCrustScheduleColorList[0].ArticleColorNo;
                                                        objStockItem.GradeRange = objScheduleDrum.GradeRange;
                                                        //objStockItem.GradeID = Convert.ToByte(objScheduleDrum.GradeID);

                                                        if (string.IsNullOrEmpty(objScheduleDrum.AreaUnitName))
                                                            objStockItem.AreaUnit = null;
                                                        else
                                                            objStockItem.AreaUnit = Convert.ToByte(_context.Sys_Unit.Where(m => m.UnitName == objScheduleDrum.AreaUnitName).FirstOrDefault().UnitID);

                                                        objStockItem.QCStatus = "PCM";// Production Completed

                                                        objStockItem.OpeningPcs = 0;
                                                        objStockItem.ReceivePcs = objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs;
                                                        objStockItem.IssuePcs = 0;
                                                        objStockItem.ClosingProductionPcs = objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs;

                                                        objStockItem.OpeningSide = 0;
                                                        objStockItem.ReceiveSide = objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide;
                                                        objStockItem.IssueSide = 0;
                                                        objStockItem.ClosingProductionSide = objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide;

                                                        objStockItem.OpeningArea = 0;
                                                        objStockItem.ReceiveArea = objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea;
                                                        objStockItem.IssueArea = 0;
                                                        objStockItem.ClosingProductionArea = objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea;

                                                        _context.PRD_CrustLeatherProductionStock.Add(objStockItem);
                                                        _context.SaveChanges();
                                                    }
                                                    else
                                                    {
                                                        var LastItemInfo = (from i in _context.PRD_CrustLeatherProductionStock
                                                                            where i.StoreID == model.ProductionFloor
                                                                            && i.BuyerID == model.BuyerID
                                                                            && i.BuyerOrderID == model.BuyerOrderID
                                                                            && i.ArticleID == model.ArticleID
                                                                            && i.ItemTypeID == model.ItemTypeID
                                                                            && i.LeatherStatusID == model.LeatherStatusID
                                                                            && i.ColorID == colorid
                                                                            && i.ArticleChallanID == model.ArticleChallanID
                                                                                //&& i.GradeID == objScheduleDrum.GradeID
                                                                                //&& i.ArticleChallanNo == model.ArticleChallanNo
                                                                                //&& i.ArticleColorNo == model.PrdYearMonthCrustScheduleColorList[0].ArticleColorNo
                                                                            && i.QCStatus == "PCM"
                                                                            orderby i.TransectionID descending
                                                                            select i).FirstOrDefault();

                                                        PRD_CrustLeatherProductionStock objStockItem = new PRD_CrustLeatherProductionStock();

                                                        if (model.BuyerID == null)
                                                            objStockItem.BuyerID = null;
                                                        else
                                                            objStockItem.BuyerID = Convert.ToInt32(model.BuyerID);
                                                        if (model.BuyerOrderID == null)
                                                            objStockItem.BuyerOrderID = null;
                                                        else
                                                            objStockItem.BuyerOrderID = Convert.ToInt32(model.BuyerOrderID);
                                                        objStockItem.StoreID = Convert.ToByte(model.ProductionFloor);
                                                        objStockItem.ArticleID = Convert.ToInt32(model.ArticleID);
                                                        objStockItem.ItemTypeID = Convert.ToByte(model.ItemTypeID);
                                                        objStockItem.LeatherTypeID = Convert.ToByte(_context.Sys_LeatherType.Where(m => m.LeatherTypeName == "Crust").FirstOrDefault().LeatherTypeID);
                                                        objStockItem.LeatherStatusID = Convert.ToByte(model.LeatherStatusID);

                                                        objStockItem.ColorID = Convert.ToInt32(colorid);
                                                        objStockItem.GradeRange = objScheduleDrum.GradeRange;
                                                        //objStockItem.GradeID = Convert.ToByte(objScheduleDrum.GradeID);
                                                        objStockItem.ArticleChallanID = model.ArticleChallanID;
                                                        objStockItem.ArticleChallanNo = model.ArticleChallanNo;
                                                        objStockItem.ArticleColorNo = model.PrdYearMonthCrustScheduleColorList[0].ArticleColorNo;

                                                        objStockItem.QCStatus = "PCM";// Production Completed

                                                        objStockItem.OpeningPcs = LastItemInfo.ClosingProductionPcs;
                                                        objStockItem.ReceivePcs = objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs;
                                                        objStockItem.IssuePcs = 0;
                                                        objStockItem.ClosingProductionPcs = LastItemInfo.ClosingProductionPcs + (objScheduleDrum.DrumPcs == null ? 0 : objScheduleDrum.DrumPcs);

                                                        objStockItem.OpeningSide = LastItemInfo.ClosingProductionSide;
                                                        objStockItem.ReceiveSide = objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide;
                                                        objStockItem.IssueSide = 0;
                                                        objStockItem.ClosingProductionSide = LastItemInfo.ClosingProductionSide + (objScheduleDrum.DrumSide == null ? 0 : objScheduleDrum.DrumSide);

                                                        objStockItem.OpeningArea = LastItemInfo.ClosingProductionArea;
                                                        objStockItem.ReceiveArea = objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea;
                                                        objStockItem.IssueArea = 0;
                                                        objStockItem.ClosingProductionArea = LastItemInfo.ClosingProductionArea + (objScheduleDrum.DrumArea == null ? 0 : objScheduleDrum.DrumArea);

                                                        _context.PRD_CrustLeatherProductionStock.Add(objStockItem);
                                                        _context.SaveChanges();
                                                    }

                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        if (stockError == 0)
                        {
                            _context.SaveChanges();
                            tx.Complete();
                            _vmMsg.Type = Enums.MessageType.Success;
                            _vmMsg.Msg = "Execute Successfully.";
                        }
                        else if (stockError == 1)
                        {
                            //_context.SaveChanges();
                            //tx.Complete();
                            _vmMsg.Type = Enums.MessageType.Error;
                            _vmMsg.Msg = "Item does not exit in Stock.";
                        }
                        else if (stockError == 2)
                        {
                            //_context.SaveChanges();
                            //tx.Complete();
                            _vmMsg.Type = Enums.MessageType.Error;
                            _vmMsg.Msg = "Not Enoung Quantity in Stock.";
                        }
                        //_context.SaveChanges();
                    }
                    //if (stockError == 0)
                    //{
                    //    tx.Complete();
                    //    _vmMsg.Type = Enums.MessageType.Success;
                    //    _vmMsg.Msg = "Execute Successfully.";
                    //}
                    //else if (stockError == 1)
                    //{
                    //    //_context.SaveChanges();
                    //    tx.Complete();
                    //    _vmMsg.Type = Enums.MessageType.Error;
                    //    _vmMsg.Msg = "Item does not exit in Stock.";
                    //}
                    //else if (stockError == 2)
                    //{
                    //    //_context.SaveChanges();
                    //    tx.Complete();
                    //    _vmMsg.Type = Enums.MessageType.Error;
                    //    _vmMsg.Msg = "Not Enoung Quantity in Stock.";
                    //}
                }
            }
            catch
            {
                _vmMsg.Type = Enums.MessageType.Error;
                _vmMsg.Msg = "Failed to Execute.";
            }
            return _vmMsg;
        }

        public ValidationMsg DeletedCrustYearMonthSchedule(string YearMonID, string ScheduleID, string ScheduleDateID, string ScheduleItemID, string RecordStatus)
        {
            _vmMsg = new ValidationMsg();
            try
            {
                long scheduleItemID = Convert.ToInt64(ScheduleItemID);
                //var scheduleID = _context.PRD_YearMonthSchedule.Where(m => m.ScheduleNo == ScheduleNo).FirstOrDefault().ScheduleID;
                var scheduleList = _context.PRD_YearMonthFinishScheduleColor.Where(m => m.ScheduleItemID == scheduleItemID).ToList();
                if (scheduleList.Count > 0)
                {
                    _vmMsg.Type = Enums.MessageType.Error;
                    _vmMsg.Msg = "Child Record Found.";
                }
                else
                {
                    var deleteYearMonthScheduleItem = _context.PRD_YearMonthCrustScheduleItem.First(m => m.ScheduleItemID == scheduleItemID);
                    _context.PRD_YearMonthCrustScheduleItem.Remove(deleteYearMonthScheduleItem);

                    long scheduleDateID = Convert.ToInt64(ScheduleDateID);
                    var deleteYearMonthScheduleDate = _context.PRD_YearMonthScheduleDate.First(m => m.ScheduleDateID == scheduleDateID);
                    _context.PRD_YearMonthScheduleDate.Remove(deleteYearMonthScheduleDate);

                    long scheduleID = Convert.ToInt64(ScheduleID);
                    var deleteYearMonthSchedule = _context.PRD_YearMonthSchedule.First(m => m.ScheduleID == scheduleID);
                    _context.PRD_YearMonthSchedule.Remove(deleteYearMonthSchedule);

                    long yearMonID = Convert.ToInt64(YearMonID);
                    var deleteYearMonth = _context.PRD_YearMonth.First(m => m.YearMonID == yearMonID);
                    _context.PRD_YearMonth.Remove(deleteYearMonth);

                    _context.SaveChanges();

                    _vmMsg.Type = Enums.MessageType.Success;
                    _vmMsg.Msg = "Deleted Successfully.";
                }
            }
            catch
            {
                _vmMsg.Type = Enums.MessageType.Error;
                _vmMsg.Msg = "Failed to Delete.";
            }
            return _vmMsg;
        }

        public ValidationMsg DeletedCrustScheduleColor(long SdulItemColorID, string RecordStatus)
        {
            _vmMsg = new ValidationMsg();
            try
            {
                if (RecordStatus != "CNF")
                {
                    var scheduleList = _context.PRD_YearMonthCrustScheduleDrum.Where(m => m.SdulItemColorID == SdulItemColorID).ToList();
                    if (scheduleList.Count > 0)
                    {
                        _vmMsg.Type = Enums.MessageType.Error;
                        _vmMsg.Msg = "Child Record Found.";
                    }
                    else
                    {
                        var deleteElement = _context.PRD_YearMonthCrustScheduleColor.First(m => m.SdulItemColorID == SdulItemColorID);
                        _context.PRD_YearMonthCrustScheduleColor.Remove(deleteElement);
                        _context.SaveChanges();

                        _vmMsg.Type = Enums.MessageType.Success;
                        _vmMsg.Msg = "Deleted Successfully.";
                    }
                }
                else
                {
                    _vmMsg.Type = Enums.MessageType.Error;
                    _vmMsg.Msg = "Confirmed Record Can not be Deleted.";
                }
            }
            catch
            {
                _vmMsg.Type = Enums.MessageType.Error;
                _vmMsg.Msg = "Failed to Delete.";
            }
            return _vmMsg;
        }

        public ValidationMsg DeletedScheduleDrum(long CrustSdulDrumID, string RecordStatus)
        {
            _vmMsg = new ValidationMsg();
            try
            {
                var SdulItemColorID = _context.PRD_YearMonthCrustScheduleDrum.Where(m => m.CrustSdulDrumID == CrustSdulDrumID).FirstOrDefault().SdulItemColorID;
                var ProductionStatus = _context.PRD_YearMonthCrustScheduleColor.Where(m => m.SdulItemColorID == SdulItemColorID).FirstOrDefault().ProductionStatus;
                if (RecordStatus != "CNF")
                {
                    if (ProductionStatus == "SCH")
                    {
                        var deleteElement = _context.PRD_YearMonthCrustScheduleDrum.First(m => m.CrustSdulDrumID == CrustSdulDrumID);
                        _context.PRD_YearMonthCrustScheduleDrum.Remove(deleteElement);

                        _context.SaveChanges();
                        _vmMsg.Type = Enums.MessageType.Success;
                        _vmMsg.Msg = "Deleted Successfully.";
                    }
                    else
                    {
                        _vmMsg.Type = Enums.MessageType.Error;
                        _vmMsg.Msg = "Running/Postpond/Completed Record Can not be Deleted.";
                    }
                }
                else
                {
                    _vmMsg.Type = Enums.MessageType.Error;
                    _vmMsg.Msg = "Confirmed Record Can not be Deleted.";
                }
            }
            catch
            {
                _vmMsg.Type = Enums.MessageType.Error;
                _vmMsg.Msg = "Failed to Delete.";
            }
            return _vmMsg;
        }

        public List<PrdYearMonthCrustScheduleItem> GetCrustProductionScheduleItemForSearch()
        {
            List<PRD_YearMonthCrustScheduleItem> searchList = _context.PRD_YearMonthCrustScheduleItem.OrderByDescending(m => m.ScheduleItemID).ToList();
            return searchList.Select(c => SetToBussinessItemObject(c)).ToList<PrdYearMonthCrustScheduleItem>();
        }

        public PrdYearMonthCrustScheduleItem SetToBussinessItemObject(PRD_YearMonthCrustScheduleItem Entity)
        {
            PrdYearMonthCrustScheduleItem Model = new PrdYearMonthCrustScheduleItem();

            Model.ScheduleItemID = Entity.ScheduleItemID;
            Model.ScheduleProductionNo = Entity.ScheduleProductionNo;
            Model.BuyerID = Entity.BuyerID;
            Model.BuyerName = Entity.BuyerID == null ? "" : _context.Sys_Buyer.Where(m => m.BuyerID == Entity.BuyerID).FirstOrDefault().BuyerName;
            Model.BuyerOrderID = Entity.BuyerOrderID;
            Model.BuyerOrderNo = Entity.BuyerOrderID == null ? "" : _context.SLS_BuyerOrder.Where(m => m.BuyerOrderID == Entity.BuyerOrderID).FirstOrDefault().BuyerOrderNo;
            Model.ArticleID = Entity.ArticleID;
            Model.ArticleName = Entity.ArticleID == null ? "" : _context.Sys_Article.Where(m => m.ArticleID == Entity.ArticleID).FirstOrDefault().ArticleName;
            Model.ArticleChallanID = Entity.ArticleChallanID;
            Model.ArticleChallanNo = Entity.ArticleChallanNo;
            Model.AvgSize = Entity.AvgSize;
            Model.AvgSizeUnit = Entity.AvgSizeUnit;
            Model.SelectionRange = Entity.SelectionRange;
            Model.SideDescription = Entity.SideDescription;
            Model.Thickness = Entity.Thickness;
            Model.ThicknessUnit = Entity.ThicknessUnit;
            Model.ThicknessAt = Entity.ThicknessAt;
            Model.ItemTypeID = Entity.ItemTypeID;
            Model.LeatherStatusID = Entity.LeatherStatusID;
            Model.LeatherTypeID = Entity.LeatherTypeID;
            Model.LeatherStatusID = Entity.LeatherStatusID;
            Model.LeatherTypeID = Entity.LeatherTypeID;

            return Model;
        }

        public List<PrdYearMonthCrustScheduleDrum> GetGradeInfoInProductionFloor(string ProductionFloor, string BuyerID, string BuyerOrderID, string ArticleID, string ItemTypeID, string LeatherStatusID, string ArticleChallanID)
        {
            if (!string.IsNullOrEmpty(ProductionFloor))
            {
                if (!string.IsNullOrEmpty(BuyerID))
                {
                    if (string.IsNullOrEmpty(BuyerOrderID))
                    {
                        var query = @"select inv.TransectionID,inv.GradeRange,inv.ClosingProductionPcs ProductionPcs,inv.ClosingProductionSide ProductionSide,inv.ClosingProductionArea ProductionArea from dbo.PRD_CrustLeatherProductionStock inv
                     INNER JOIN (select MAX(TransectionID)TransectionID,BuyerID,BuyerOrderID,StoreID,ItemTypeID,LeatherStatusID,ArticleID,ArticleChallanID from dbo.PRD_CrustLeatherProductionStock
                     where QCStatus='REP' group by StoreID,BuyerID,BuyerOrderID,ItemTypeID,LeatherStatusID,ArticleID,ArticleChallanID) sup
                     ON inv.TransectionID=sup.TransectionID
                     where inv.StoreID = " + ProductionFloor + " and inv.BuyerID = " + BuyerID + " and inv.BuyerOrderID IS NULL and inv.ItemTypeID = " + ItemTypeID + " and inv.LeatherStatusID = " + LeatherStatusID + " and inv.ArticleID = " + ArticleID + " and inv.ArticleChallanID = " + ArticleChallanID + " and inv.QCStatus='REP' and (inv.ClosingProductionPcs>0 or inv.ClosingProductionSide>0 or inv.ClosingProductionArea>0)";
                        var allData = _context.Database.SqlQuery<PrdYearMonthCrustScheduleDrum>(query).ToList();
                        List<PrdYearMonthCrustScheduleDrum> searchList = allData.ToList();
                        return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdYearMonthCrustScheduleDrum>();
                    }
                    else
                    {
                        var query = @"select inv.TransectionID,inv.GradeRange,inv.ClosingProductionPcs ProductionPcs,inv.ClosingProductionSide ProductionSide,inv.ClosingProductionArea ProductionArea from dbo.PRD_CrustLeatherProductionStock inv
                     INNER JOIN (select MAX(TransectionID)TransectionID,BuyerID,BuyerOrderID,StoreID,ItemTypeID,LeatherStatusID,ArticleID,ArticleChallanID from dbo.PRD_CrustLeatherProductionStock
                     where QCStatus='REP' group by StoreID,BuyerID,BuyerOrderID,ItemTypeID,LeatherStatusID,ArticleID,ArticleChallanID) sup
                     ON inv.TransectionID=sup.TransectionID
                     where inv.StoreID = " + ProductionFloor + " and inv.BuyerID = " + BuyerID + " and inv.BuyerOrderID = " + BuyerOrderID + " and inv.ItemTypeID = " + ItemTypeID + " and inv.LeatherStatusID = " + LeatherStatusID + " and inv.ArticleID = " + ArticleID + " and inv.ArticleChallanID = " + ArticleChallanID + " and inv.QCStatus='REP' and (inv.ClosingProductionPcs>0 or inv.ClosingProductionSide>0 or inv.ClosingProductionArea>0)";
                        var allData = _context.Database.SqlQuery<PrdYearMonthCrustScheduleDrum>(query).ToList();
                        List<PrdYearMonthCrustScheduleDrum> searchList = allData.ToList();
                        return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdYearMonthCrustScheduleDrum>();
                    }
                }
                else
                {
                    var query = @"select inv.TransectionID,inv.GradeRange,inv.ClosingProductionPcs ProductionPcs,inv.ClosingProductionSide ProductionSide,inv.ClosingProductionArea ProductionArea from dbo.PRD_CrustLeatherProductionStock inv
                     INNER JOIN (select MAX(TransectionID)TransectionID,BuyerID,BuyerOrderID,StoreID,ItemTypeID,LeatherStatusID,ArticleID,ArticleChallanID from dbo.PRD_CrustLeatherProductionStock
                     where QCStatus='REP' group by StoreID,BuyerID,BuyerOrderID,ItemTypeID,LeatherStatusID,ArticleID,ArticleChallanID) sup
                     ON inv.TransectionID=sup.TransectionID
                     where inv.StoreID = " + ProductionFloor + " and inv.BuyerID IS NULL and inv.BuyerOrderID  IS NULL and inv.ItemTypeID = " + ItemTypeID + " and inv.LeatherStatusID = " + LeatherStatusID + " and inv.ArticleID = " + ArticleID + " and inv.ArticleChallanID = " + ArticleChallanID + " and inv.QCStatus='REP' and (inv.ClosingProductionPcs>0 or inv.ClosingProductionSide>0 or inv.ClosingProductionArea>0)";
                    var allData = _context.Database.SqlQuery<PrdYearMonthCrustScheduleDrum>(query).ToList();
                    List<PrdYearMonthCrustScheduleDrum> searchList = allData.ToList();
                    return searchList.Select(c => SetToBussinessObject(c)).ToList<PrdYearMonthCrustScheduleDrum>();
                }
            }
            else
                return null;
        }

        public PrdYearMonthCrustScheduleDrum SetToBussinessObject(PrdYearMonthCrustScheduleDrum Entity)
        {
            PrdYearMonthCrustScheduleDrum Model = new PrdYearMonthCrustScheduleDrum();

            //Model.ArticleChallanNo = Entity.ArticleChallanNo;
            Model.GradeRange = Entity.GradeRange;
            Model.ProductionPcs = Entity.ProductionPcs;
            Model.ProductionSide = Entity.ProductionSide;
            Model.ProductionArea = Entity.ProductionArea;

            //Model.ProductionPcs = Entity.TransectionID == null ? 0 : _context.PRD_CrustLeatherProductionStock.Where(m => m.TransectionID == Entity.TransectionID).FirstOrDefault().ClosingProductionPcs;
            //Model.ProductionSide = Entity.TransectionID == null ? 0 : _context.PRD_CrustLeatherProductionStock.Where(m => m.TransectionID == Entity.TransectionID).FirstOrDefault().ClosingProductionSide;
            //Model.ProductionArea = Entity.TransectionID == null ? 0 : _context.PRD_CrustLeatherProductionStock.Where(m => m.TransectionID == Entity.TransectionID).FirstOrDefault().ClosingProductionArea;

            return Model;
        }

        public List<PrdYearMonthCrustScheduleColor> GetColorListAccordingToArticleChallan(int? _BuyerOrderItemID, long _ArticleChallanID)
        {
            using (_context)
            {
                if (_BuyerOrderItemID != null)
                {
                    var Data = (from c in _context.SLS_BuyerOrderItemColor.AsEnumerable()
                                where c.BuyerOrderItemID == _BuyerOrderItemID

                                join col in _context.Sys_Color on c.ColorID equals col.ColorID into Colors
                                from col in Colors.DefaultIfEmpty()

                                join u in _context.Sys_Unit on c.ColorUnit equals u.UnitID into Units
                                from u in Units.DefaultIfEmpty()

                                join a in _context.Sys_ArticleChallanColor.Where(x => x.ArticleChallanID == _ArticleChallanID) on c.ColorID equals a.ArticleColor into ArticleColors
                                from a in ArticleColors.DefaultIfEmpty()

                                orderby (a == null ? null : a.ArticleColorNo) ascending
                                select new PrdYearMonthCrustScheduleColor
                                {
                                    //SdulItemColorID = c.SdulItemColorID,
                                    ColorID = c.ColorID,
                                    ArticleColorNo = (a == null ? null : a.ArticleColorNo),
                                    ColorName = (col == null ? null : col.ColorName),
                                    ColorArea = c.ColorQty,
                                    //ColorPCS = c.ColorQty,
                                    //ColorSide = c.ColorSide,
                                    //Remarks = c.Remarks,
                                    AreaUnit = c.ColorUnit,
                                    AreaUnitName = "SFT",
                                    WeightUnitName = "KG",
                                    ProductionStatus = "Schedule"
                                }).ToList();

                    return Data;
                }

                else
                {
                    var Data = (from c in _context.Sys_ArticleChallanColor.AsEnumerable()
                                where c.ArticleChallanID == _ArticleChallanID

                                join col in _context.Sys_Color on c.ArticleColor equals col.ColorID into Colors
                                from col in Colors.DefaultIfEmpty()

                                orderby c.ArticleColorNo ascending
                                select new PrdYearMonthCrustScheduleColor
                                {
                                    ColorID = c.ArticleColor,
                                    ColorName = (col == null ? null : col.ColorName),
                                    ArticleColorNo = c.ArticleColorNo,
                                    AreaUnitName = "SFT",
                                    WeightUnitName = "KG",
                                    ProductionStatus = "Schedule"
                                }).ToList();

                    return Data;
                }

            }
        }
    }
}
