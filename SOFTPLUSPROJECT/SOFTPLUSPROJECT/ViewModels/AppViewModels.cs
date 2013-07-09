using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFTPLUSPROJECT.Models;

namespace SOFTPLUSPROJECT.ViewModels
{
    public class MasterDetailInfoViewModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public DateTime? LoadDate { get; set; }
    }

    public class DetailInfoViewModel
    {
        public int DetailInfoId { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public float Quantity { get; set; }

        public int ItemSizeId { get; set; }

        public int ItemSizeName { get; set; }
    }

    public class MasterDetailViewModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public DateTime LoadDate { get; set; }

        public int ItemSizeId { get; set; }

        public int ItemSizeName { get; set; }

        public float Quantity { get; set; }

        public IEnumerable<DetailInfoModel> DetailInfos { get; set; }
        public IEnumerable<ItemSizeModel> ddlItemSizes { get; set; }
    }

    public class OfflineMasterDetailViewModel
    {
        public MasterDetailInfoViewModel Master { get; set; }
        public IEnumerable<DetailInfoViewModel> Details { get; set; }
    }

    public class JsonResult
    {
        public string msg { get; set; }

        public string status { get; set; }
    }
}
