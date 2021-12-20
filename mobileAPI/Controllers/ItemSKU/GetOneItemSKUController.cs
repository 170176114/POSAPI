using mobileAPI.Models.Request;
using mobileAPI.Models.Response;
using mobileAPI.ODBCDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace mobileAPI.Controllers.ItemSKU
{
    public class GetOneItemSKUController : ApiController
    {
        Itemdb itemdb = new Itemdb();
        [HttpPost]
        [ActionName("GetOneItem")]
        public ResponseItemSKU GetOneItemSKU(RequsetItem requestItem)
        {
            ResponseItemSKU itemSKU = new ResponseItemSKU();

            itemSKU = itemdb.getItem(requestItem);
            if (itemSKU.company_id == null)
            {
                itemSKU.status = 0;
            }
            else
            {
                itemSKU.status = 1;
            }
            return itemSKU;
        }
    }
}