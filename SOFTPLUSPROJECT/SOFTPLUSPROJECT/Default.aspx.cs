using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using SOFTPLUSPROJECT.ViewModels;
using SOFTPLUSPROJECT.Helpers;
using SOFTPLUSPROJECT.Models;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SOFTPLUSPROJECT
{
    public partial class _Default : System.Web.UI.Page
    {
        private static SoftplusTextingEntities _db = new SoftplusTextingEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CustomerId.Value = GetLastMasterId().ToString();

                DataBindToItemSize();

                if (String.IsNullOrEmpty(GetConnectionStringFromCookie()))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }

        #region Web Method

        [WebMethod]
        public static JsonResult Add(DetailInfoViewModel detailInfoViewModel)
        {
            JsonResult jsonResult = new JsonResult();

            try
            {

                if (detailInfoViewModel.CustomerId != 0 && detailInfoViewModel.ItemSizeId != 0 && detailInfoViewModel.Quantity != 0)
                {
                    jsonResult.msg = Boolean.TrueString;
                    jsonResult.status = MessageType.success.ToString();
                    return jsonResult;
                }

                jsonResult.msg = "Data is required";
                jsonResult.status = MessageType.info.ToString();
                return jsonResult;

            }
            catch (Exception ex)
            {
                jsonResult.msg = ExceptionHelper.ExceptionMessageFormat(ex);
                jsonResult.status = MessageType.error.ToString();
                return jsonResult;
            }
        }

        [WebMethod]
        public static JsonResult AjaxSave(MasterDetailInfoViewModel model, List<DetailInfoViewModel> modelList)
        {
            JsonResult jsonResult = new JsonResult();

            try
            {
                if (model.CustomerId != 0 && model.CustomerName != null && model.Address != null)
                {

                    if (model != null)
                    {
                        if (modelList.Any())
                        {
                            //Save MasterDetails Data

                            MasterDetailInfoModel masterDetailInfoModel = new MasterDetailInfoModel()
                            {
                                CustomerID = model.CustomerId,
                                CustomerName = model.CustomerName,
                                Address = model.Address,
                                LoadDate = DateTime.Now
                            };

                            List<DetailInfoModel> detailInfoModelList = new List<DetailInfoModel>();


                            ////Master Data Insert
                            //if (InsertToMasterDetailInfo(masterDetailInfoModel) > 0)
                            //{
                            //var lastInsetMasterDetailInfo = _db.tbl_MasterDetailInfo.FirstOrDefault(x => x.CustomerID == model.CustomerId && x.CustomerName == model.CustomerName);

                            ////Details Data Insert
                            //if (lastInsetMasterDetailInfo != null)
                            //{
                            foreach (var item in modelList)
                            {
                                DetailInfoModel detailInfoModel = new DetailInfoModel()
                                {
                                    //CustomerID = model.CustomerId,
                                    ItemSizeID = item.ItemSizeId,
                                    Quantity = item.Quantity
                                };

                                //if (InsertToDetailInfo(detailInfoModel) > 0)
                                //{
                                //    continue;
                                //}
                                //else
                                //{
                                //    break;
                                //}

                                detailInfoModelList.Add(detailInfoModel);
                            }
                            //}
                            //}

                            //Save MasterDetails Data

                            if (InsertMasterDetailInfoBySP(masterDetailInfoModel, detailInfoModelList))
                            {
                                jsonResult.msg = "Data saved successfully to server.";
                                jsonResult.status = MessageType.success.ToString();
                                return jsonResult;
                            }
                            else
                            {
                                jsonResult.msg = "Data can not saved successfully to server.";
                                jsonResult.status = MessageType.warn.ToString();
                                return jsonResult;
                            }
                        }
                        else
                        {
                            //Details Data Null Message
                            jsonResult.msg = "Details data could not found.";
                            jsonResult.status = MessageType.warn.ToString();
                            return jsonResult;
                        }
                    }
                    else
                    {
                        //Master Data Null Message
                        jsonResult.msg = "Master data could not found.";
                        jsonResult.status = MessageType.warn.ToString();
                        return jsonResult;
                    }

                }

                jsonResult.msg = "Data is required";
                jsonResult.status = MessageType.info.ToString();
                return jsonResult;

            }
            catch (Exception ex)
            {
                jsonResult.msg = ExceptionHelper.ExceptionMessageFormat(ex);
                jsonResult.status = MessageType.error.ToString();
                return jsonResult;
            }
        }

        [WebMethod]
        public static JsonResult OfflineAjaxSave(List<OfflineMasterDetailViewModel> offlineModelList)
        {
            JsonResult jsonResult = new JsonResult();

            try
            {

                if (offlineModelList != null)
                {
                    foreach (var offlineMasterDetailViewModel in offlineModelList)
                    {
                        if (offlineMasterDetailViewModel.Details.Any())
                        {
                            //Save MasterDetails Data

                            //Save MasterDetails Data

                            jsonResult.msg = "Data saved successfully to server.";
                            jsonResult.status = MessageType.success.ToString();
                            return jsonResult;
                        }
                        else
                        {
                            //Details Data Null Message
                            jsonResult.msg = "Details data could not found.";
                            jsonResult.status = MessageType.warn.ToString();
                            return jsonResult;
                        }
                    }

                }

                //Data Null Message
                jsonResult.msg = "Data could not found.";
                jsonResult.status = MessageType.warn.ToString();
                return jsonResult;

            }
            catch (Exception ex)
            {
                jsonResult.msg = ExceptionHelper.ExceptionMessageFormat(ex);
                jsonResult.status = MessageType.error.ToString();
                return jsonResult;
            }
        }


        #endregion

        #region My Methods

        /// <summary>
        /// Insert Master DetailInfo By SP
        /// </summary>
        /// <param name="masterDetailInfoModel"></param>
        /// <param name="detailInfoModelList"></param>
        /// <returns></returns>
        private static bool InsertMasterDetailInfoBySP(MasterDetailInfoModel masterDetailInfoModel, List<DetailInfoModel> detailInfoModelList)
        {
            bool isInsert = false;


            if (masterDetailInfoModel != null && detailInfoModelList.Any())
            {

                SqlConnection sqlConnection = null;

                try
                {

                    string dbConnectionString = ConfigurationManager.ConnectionStrings["SoftplusTextingConStr"].ConnectionString;
                    sqlConnection = new SqlConnection(dbConnectionString);

                    SqlCommand command = new SqlCommand("sp_InsertMasterDetails", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter parmCustomerName = new SqlParameter("@customerName", SqlDbType.VarChar, 50);
                    parmCustomerName.Value = masterDetailInfoModel.CustomerName;
                    command.Parameters.Add(parmCustomerName);

                    SqlParameter parmAddress = new SqlParameter("@address", SqlDbType.VarChar, 200);
                    parmAddress.Value = masterDetailInfoModel.Address;
                    command.Parameters.Add(parmAddress);

                    SqlParameter parmLoadDate = new SqlParameter("@loadDate", SqlDbType.SmallDateTime);
                    parmLoadDate.Value = masterDetailInfoModel.LoadDate;
                    command.Parameters.Add(parmLoadDate);


                    // declare a table to store the parameter values 
                    DataTable detailItems = new DataTable();
                    detailItems.Columns.Add("CustomerID", typeof(int));
                    detailItems.Columns.Add("ItemSizeID", typeof(int));
                    detailItems.Columns.Add("Quantity", typeof(double));

                    foreach (var item in detailInfoModelList)
                    {
                        detailItems.Rows.Add(new object[] { null, item.ItemSizeID, item.Quantity });
                    }

                    // add the table as a parameter to the stored procedure 
                    SqlParameter paramDetailItems = command.Parameters.AddWithValue("@detailItems", detailItems);
                    paramDetailItems.SqlDbType = SqlDbType.Structured;
                    paramDetailItems.TypeName = "dbo.tbl_DetailInfoType";
                    //command.Parameters.Add(paramDetailItems);

                    SqlParameter parmResult = new SqlParameter("@result", SqlDbType.NVarChar, 100);
                    parmResult.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parmResult);

                    SqlParameter parmMessage = new SqlParameter("@message", SqlDbType.NVarChar, 100);
                    parmMessage.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parmMessage);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();


                    string result = command.Parameters["@result"].Value.ToString();

                    if (!String.IsNullOrEmpty(result) && result == "Fail")
                    {
                        return isInsert;

                    }
                    else
                    {
                        return isInsert = true;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();

                }

            }


            return isInsert;
        }

        /// <summary>
        /// Insert Method for Insert tbl_MasterDetailInfo Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static int InsertToMasterDetailInfo(MasterDetailInfoModel model)
        {
            try
            {
                tbl_MasterDetailInfo entity = new tbl_MasterDetailInfo()
                {
                    CustomerID = model.CustomerID,
                    CustomerName = model.CustomerName,
                    Address = model.Address,
                    LoadDate = model.LoadDate
                };
                //Add the object to Entity Set
                _db.AddTotbl_MasterDetailInfo(entity);
                //Save the Entity Set
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert Method for Insert tbl_DetailInfo Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static int InsertToDetailInfo(DetailInfoModel model)
        {
            try
            {
                tbl_MasterDetailInfo masterDetailInfo = _db.tbl_MasterDetailInfo.FirstOrDefault(x => x.CustomerID == model.CustomerID);
                tbl_ItemSize itemSize = _db.tbl_ItemSize.FirstOrDefault(x => x.ItemSizeID == model.ItemSizeID);
                tbl_DetailInfo entity = new tbl_DetailInfo()
                {
                    CustomerID = model.CustomerID,
                    ItemSizeID = model.ItemSizeID,
                    Quantity = model.Quantity,
                    tbl_ItemSize = itemSize,
                    tbl_MasterDetailInfo = masterDetailInfo
                };
                //Add the object to Entity Set
                _db.AddTotbl_DetailInfo(entity);
                //Save the Entity Set
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Item Size Dropdownlist Data Bind
        /// </summary>
        private void DataBindToItemSize()
        {
            var itemSizes = _db.tbl_ItemSize.ToList();

            this.ItemSizeId.DataSource = itemSizes;
            this.ItemSizeId.DataTextField = "ItemSize";
            this.ItemSizeId.DataValueField = "ItemSizeID";
            this.ItemSizeId.DataBind();
        }

        /// <summary>
        /// Get Last Id of tbl_MasterDetailInfo. Note: We should generate auto and unique
        /// </summary>
        /// <returns></returns>
        private int GetLastMasterId()
        {
            int lastId = 0;

            var masterDetailInfos = _db.tbl_MasterDetailInfo.ToList();

            var masterDetailInfo = masterDetailInfos.LastOrDefault();
            if (masterDetailInfo != null)
            {
                var masterDetailInfoLastId = masterDetailInfo.CustomerID;

                lastId = masterDetailInfoLastId != 0 ? masterDetailInfoLastId + 1 : 0;
            }

            return lastId;
        }

        /// <summary>
        /// Get Connection String from Cookie
        /// </summary>
        /// <returns></returns>
        private string GetConnectionStringFromCookie()
        {
            string connectionString = string.Empty;


            if (Request.Cookies["CookieConStr"] != null)
            {
                string conStr = Request.Cookies["CookieConStr"].Value.ToString();
                connectionString = conStr;
            }

            return connectionString;
        }

        #endregion
    }
}
