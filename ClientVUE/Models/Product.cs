﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Client.Models
{


    public partial class Product
    {

        public Product()
        {
           // ModifiedDate = DateTime.Now.ToString(new CultureInfo("fi-FI"));
        }

        public Product(long productId)
        {
            ProductId = productId;
        }

        [Display(Name = "Pruduct ID")]
        public long ProductId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Pruduct number")]
        public string ProductNumber { get; set; }

        [Display(Name = "Make flag")]
        public bool? MakeFlag { get; set; }

        [Display(Name = "Finished goods flag")]
        public bool? FinishedGoodsFlag { get; set; }

        public string Color { get; set; }

        [Display(Name = "Safety stock Level")]
        public short SafetyStockLevel { get; set; }

        [Display(Name = "Reorder point")]
        public short ReorderPoint { get; set; }

        [Display(Name = "Standard cost")]
        // [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public decimal StandardCost { get; set; }

        [Display(Name = "List price")]
        // [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public decimal ListPrice { get; set; }

        public string Size { get; set; }

        [Display(Name = "Size unit measure code")]
        public string SizeUnitMeasureCode { get; set; }

        [Display(Name = "Weight unit measure code")]
        public string WeightUnitMeasureCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? Weight { get; set; }

        [Display(Name = "Days to manufacture")]
        public int DaysToManufacture { get; set; }

        [Display(Name = "Product line")]
        public string ProductLine { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        [Display(Name = "Product subcategory ID")]
        public int? ProductSubcategoryId { get; set; }

        [Display(Name = "Product model ID")]
        public int? ProductModelId { get; set; }

        [DataType(DataType.Date)]
        //  [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sell start date")]
        public DateTime? SellStartDate { get; set; }

        [DataType(DataType.Date)]
        //    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sell end date")]
        public DateTime? SellEndDate { get; set; }

        [DataType(DataType.Date)]
        //   [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Discontinued date")]
        public DateTime? DiscontinuedDate { get; set; }

        public Guid Rowguid { get; set; }

        [DataType(DataType.Date)]
        //   [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified date")]
        public DateTime? ModifiedDate { get; set; }

        private string userIdentifier;

        [Display(Name = "Modified by")]
        public string UserIdentifier
        {
            get { return userIdentifier ?? "Timo"; }

            set { userIdentifier = value; }
        }

        [Display(Name = "Status")]
        public bool Online { get; set; }

        [Display(Name = "Online (X) or Offline ()?")]
     //   public string OnlineOrOffline => (this.Online) ? "Offline" : "Online";
        public string OnlineOrOffline => (this.Online) ? "Online" : "Offline";

        public bool Locked { get; set; }

        public long LockedTimeStamp { get; set;}
        public int QueueLength { get; set; }
        public string Pending { get; set; } //Pending change in database

        [Display(Name = "Product model")]
        public ProductModel ProductModel { get; set; }

        [Display(Name = "Product subcategory")]
        public ProductSubcategory ProductSubcategory { get; set; }

        [Display(Name = "Size unit measure code navigation")]
        public UnitMeasure SizeUnitMeasureCodeNavigation { get; set; }

        [Display(Name = "Weight unit measure code navigation")]
        public UnitMeasure WeightUnitMeasureCodeNavigation { get; set; }

        public Guid LockedStatusId { get; set; }
        public Guid OnlineStatusId { get; set; }
      //  public bool OldOnline { get; set; }
    }
}
