﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.EntitiesModel.OperationModel
{
    public class PRQChemLocalPurcRecvItem
    {
        public long ReceiveItemID { get; set; }
        public long? POReceiveID { get; set; }
        public long? ReceiveID { get; set; }
        public int? ItemID { get; set; }
        public int? SupplierID { get; set; }
        public string ChemicalName { get; set; }
        public string BatchNo { get; set; }
        public string LotNo { get; set; }
        public string ExpiryDate { get; set; }
        public byte? PackSizeID { get; set; }
        public string PackSize { get; set; }
        public byte? SizeUnitID { get; set; }
        public string SizeUnit { get; set; }
        public decimal? PackQty { get; set; }
        public decimal? ReceiveQty { get; set; }
        public byte? UnitID { get; set; }
        public string UnitName { get; set; }
        public int? ManufacturerID { get; set; }
        public string Manufacturer { get; set; }
        public string Remark { get; set; }
    }
}
