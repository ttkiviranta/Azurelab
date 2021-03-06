﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Read
{
    public class ProductReadNull
    {
        public ProductReadNull()
        {
            // CreationTime = DateTime.Now.ToString(new CultureInfo("en-US"));
            Rowguid = Guid.NewGuid();

        }
        public ProductReadNull(Guid rowguid) : this()
        {
            Rowguid = rowguid;
        }

        public ProductReadNull(long productId)
        {
            ProductId = productId;
        }

        [Key]
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public bool? MakeFlag { get; set; }
        public bool? FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public short SafetyStockLevel { get; set; }
        public short ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public int? ProductSubcategoryId { get; set; }
        public int? ProductModelId { get; set; }
        public DateTime? SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string UserIdentifier { get; set; }

        public Guid LockedStatusID { get; set; }
        public bool? Online { get; set; }
        public bool? Locked { get; set; }
        public long ChangeTimeStamp { get; set; }
    }
}
