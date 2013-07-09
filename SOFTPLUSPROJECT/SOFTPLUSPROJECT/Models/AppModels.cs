using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFTPLUSPROJECT.Models
{
    public class SpecificationModel
    {
        public int SpecId { get; set; }

        public string SQLServer { get; set; }

        public string DBName { get; set; }
    }

    public class InternetUserModel
    {
        public int InetUserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int SpecId { get; set; }

        public virtual SpecificationModel SpecificationModel { get; set; }

        public virtual ICollection<IntranetUserModel> IntranetUserModels { get; set; }
    }

    public class IntranetUserModel
    {
        public int SrvUserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual ICollection<InternetUserModel> InternetUserModels { get; set; }
    }

    public class ItemSizeModel
    {
        public int ItemSizeId { get; set; }

        public int Size { get; set; }

    }

    public class MasterDetailInfoModel
    {
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public DateTime LoadDate { get; set; }
    }

    public class DetailInfoModel
    {

        public int CustomerID { get; set; }

        public float Quantity { get; set; }

        public int ItemSizeID { get; set; }

        public virtual MasterDetailInfoModel MasterDetailInfoModel { get; set; }

        public virtual ItemSizeModel ItemSizeModel { get; set; }
    }
}
