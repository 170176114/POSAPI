using mobileAPI.Models.Request;
using mobileAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace mobileAPI.ODBCDB
{
    public class Itemdb : db
    {
        public ResponseItemSKU getItem(RequsetItem requsetItem)
        {
            ResponseItemSKU itemSKU = new ResponseItemSKU();

            OdbcConnection DBConnection;
            OdbcCommand DBCommand;

            OpenConnection(out DBConnection);
            using (DBConnection)
            {
                using (DBCommand = DBConnection.CreateCommand())
                {
                    string query = " SELECT I.COMPANY_ID, I.SKU, I.ITEM_NO, I.ATTRIBUTE_1, I.ATTRIBUTE_2, ";
                    query += " I.MATRIX_ATTRIBUTE_1, I.MATRIX_ATTRIBUTE_2, I.ATTRIBUTE_1_NAME, I.ATTRIBUTE_2_NAME, ";
                    query += " I.ITEM_UDF_CODE_ID, I.DOCUMENT_ID, I.ITEM_DEPT, I.ITEM_GROUP, I.ITEM_SUBGROUP, ";
                    query += " I.ITEM_MATRIX_NAME, I.ITEM_NAME, I.ITEM_SHORT_NAME, I.ITEM_TYPE, I.UOM, ";
                    query += " I.DEF_VDR, I.DEF_LOCATION_ID, I.BIN_CODE, I.STD_COST, I.MIN_PRICE, I.LIST_PRICE, ";
                    query += " I.SAFE_QTY, I.PO_LEVEL, I.ADD_POINT_PERCENT, I.ITEM_COLOR, I.STYLE_NO, I.MAIN_QTY, ";
                    query += " I.REF_M1, I.REF_M2, I.REF_M3, I.REF_M4, I.PARTS_COLOR, I.PARTS_WEIGHT, I.PARTS_UOM, I.PARTS_LENGTH, ";
                    query += " I.PARTS_REF_C1, I.PARTS_REF_T1, I.PARTS_REF_Q1, I.PARTS_REF_W1, ";
                    query += " I.PARTS_REF_C2, I.PARTS_REF_T2, I.PARTS_REF_Q2, I.PARTS_REF_W2, ";
                    query += " I.PARTS_REF_C3, I.PARTS_REF_T3, I.PARTS_REF_Q3, I.PARTS_REF_W3, ";
                    query += " I.PARTS_REF_C4, I.PARTS_REF_T4, I.PARTS_REF_Q4, I.PARTS_REF_W4, ";
                    query += " I.EXT_FIELD01, I.EXT_FIELD02, I.EXT_FIELD03, I.EXT_FIELD04, I.EXT_FIELD05, I.EXT_FIELD06, I.EXT_FIELD07, I.EXT_FIELD08, I.EXT_FIELD09, I.EXT_FIELD10, ";
                    query += " I.SERIAL_YN, I.BATCH_YN, I.ITEM_ASM_YN, I.POINT_REDEEM_YN, I.MATRIX_ITEM_YN, I.INACTIVE_YN, ";
                    query += " I.MODIFY_USER, I.MODIFY_DATE, I.CREATE_USER, I.CREATE_DATE, I.MODIFY_FLAG, I.PURCHASER, ";
                    query += " I.POS_PAC, I.FEATURE_1, I.FEATURE_2, I.FEATURE_3, I.FEATURE_4, I.WAR_AND_COND_1, I.WAR_AND_COND_2, ";
                    query += " I.VINTAGE, I.PRODUCER, I.CLASSIFICATION, I.REGION, I.APPELLATION, I.GRAPE_VARIETY, I.COUNTRY, ";
                    query += " I.SIZE_ML, ITEM_MATRIX_NAME_1, ITEM_NAME_1, ITEM_SHORT_NAME_1, ITEM_MATRIX_NAME_2, ITEM_NAME_2, ITEM_SHORT_NAME_2, ";
                    query += " I.NON_DISC_ITEM_YN, I.COUPON_ITEM_YN, I.COUPON_EFFECTIVE_FROM, I.COUPON_EFFECTIVE_TO, I.ITEM_CUT, ";
                    query += " I.ITEM_CLARITY, I.ONLINE_YN, I.WEBSTOCK_YN, I.WEB_PRICE, I.PACKAGE_YN, ";
                    query += " I.ACCODE_01, I.ACCODE_02, I.ACCODE_03, I.ACCODE_04, I.ACCODE_05, I.ACCODE_06, I.ACCODE_07, I.ACCODE_08, I.ACCODE_09, I.ACCODE_10, I.ACCODE_11, I.ACCODE_12, I.ACCODE_13, I.ACCODE_14, I.ACCODE_15 ";
                    query += " FROM ITEM_SKU I ";
                    query += " LEFT JOIN ITEM_BARCODE B ";
                    query += " ON (I.COMPANY_ID = B.COMPANY_ID AND I.SKU = B.SKU) ";
                    query += " WHERE I.COMPANY_ID = '" + requsetItem.company_id + "' AND ( I.SKU = '" + requsetItem.sku + "' OR B.BARCODE = '" + requsetItem.sku + "') AND I.INACTIVE_YN = '0' ";



                    DBCommand.CommandText = query;
                    using (OdbcDataReader DbReader = DBCommand.ExecuteReader())
                    {
                        while (DbReader.Read())
                        {
                            GetDBData DbData = new GetDBData(DbReader);
                            itemSKU.company_id = DbData.GetString("COMPANY_ID");
                            itemSKU.sku = DbData.GetString("SKU");
                            itemSKU.item_no = DbData.GetString("ITEM_NO");
                            itemSKU.attribute_1 = DbData.GetString("ATTRIBUTE_1");
                            itemSKU.attribute_2 = DbData.GetString("ATTRIBUTE_2");
                            itemSKU.matrix_attribute_1 = DbData.GetString("MATRIX_ATTRIBUTE_1");
                            itemSKU.matrix_attribute_2 = DbData.GetString("MATRIX_ATTRIBUTE_2");
                            itemSKU.attribute_1_name = DbData.GetString("ATTRIBUTE_1_NAME");
                            itemSKU.attribute_2_name = DbData.GetString("ATTRIBUTE_2_NAME");
                            itemSKU.item_udf_code_id = DbData.GetString("ITEM_UDF_CODE_ID");
                            itemSKU.document_id = DbData.GetString("DOCUMENT_ID");
                            itemSKU.item_dept = DbData.GetString("ITEM_DEPT");
                            itemSKU.item_group = DbData.GetString("ITEM_GROUP");
                            itemSKU.item_subgroup = DbData.GetString("ITEM_SUBGROUP");
                            itemSKU.item_matrix_name = DbData.GetString("ITEM_MATRIX_NAME");
                            itemSKU.item_name = DbData.GetString("ITEM_NAME");
                            itemSKU.item_short_name = DbData.GetString("ITEM_SHORT_NAME");
                            itemSKU.item_type = DbData.GetString("ITEM_TYPE");
                            itemSKU.uom = DbData.GetString("UOM");
                            itemSKU.def_vdr = DbData.GetString("DEF_VDR");
                            itemSKU.def_location_id = DbData.GetString("DEF_LOCATION_ID");
                            itemSKU.bin_code = DbData.GetString("BIN_CODE");
                            itemSKU.std_code = DbData.GetDecimal("STD_COST");
                            itemSKU.min_price = DbData.GetDecimal("MIN_PRICE");
                            itemSKU.list_price = DbData.GetDecimal("LIST_PRICE");
                            itemSKU.safe_qty = DbData.GetDecimal("SAFE_QTY");
                            itemSKU.po_level = DbData.GetDecimal("PO_LEVEL");
                            itemSKU.add_point_percent = DbData.GetDecimal("ADD_POINT_PERCENT");
                            itemSKU.item_color = DbData.GetString("ITEM_COLOR");
                            itemSKU.style_no = DbData.GetString("STYLE_NO");
                            itemSKU.main_qty = DbData.GetDecimal("MAIN_QTY");
                            itemSKU.ref_m1 = DbData.GetString("REF_M1");
                            itemSKU.ref_m2 = DbData.GetString("REF_M2");
                            itemSKU.ref_m3 = DbData.GetString("REF_M3");
                            itemSKU.ref_m4 = DbData.GetString("REF_M4");
                            itemSKU.parts_color = DbData.GetString("PARTS_COLOR");
                            itemSKU.parts_weight = DbData.GetString("PARTS_WEIGHT");
                            itemSKU.parts_uom = DbData.GetString("PARTS_UOM");
                            itemSKU.parts_length = DbData.GetString("PARTS_LENGTH");
                            itemSKU.parts_ref_c1 = DbData.GetString("PARTS_REF_C1");
                            itemSKU.parts_ref_t1 = DbData.GetString("PARTS_REF_T1");
                            itemSKU.parts_ref_q1 = DbData.GetString("PARTS_REF_Q1");
                            itemSKU.parts_ref_w1 = DbData.GetString("PARTS_REF_W1");
                            itemSKU.parts_ref_c2 = DbData.GetString("PARTS_REF_C2");
                            itemSKU.parts_ref_t2 = DbData.GetString("PARTS_REF_T2");
                            itemSKU.parts_ref_q2 = DbData.GetString("PARTS_REF_Q2");
                            itemSKU.parts_ref_w2 = DbData.GetString("PARTS_REF_W2");
                            itemSKU.parts_ref_c3 = DbData.GetString("PARTS_REF_C3");
                            itemSKU.parts_ref_t3 = DbData.GetString("PARTS_REF_T3");
                            itemSKU.parts_ref_q3 = DbData.GetString("PARTS_REF_Q3");
                            itemSKU.parts_ref_w3 = DbData.GetString("PARTS_REF_W3");
                            itemSKU.parts_ref_c4 = DbData.GetString("PARTS_REF_C4");
                            itemSKU.parts_ref_t4 = DbData.GetString("PARTS_REF_T4");
                            itemSKU.parts_ref_q4 = DbData.GetString("PARTS_REF_Q4");
                            itemSKU.parts_ref_w4 = DbData.GetString("PARTS_REF_W4");
                            itemSKU.ext_field01 = DbData.GetString("EXT_FIELD01");
                            itemSKU.ext_field02 = DbData.GetString("EXT_FIELD02");
                            itemSKU.ext_field03 = DbData.GetString("EXT_FIELD03");
                            itemSKU.ext_field04 = DbData.GetString("EXT_FIELD04");
                            itemSKU.ext_field05 = DbData.GetString("EXT_FIELD05");
                            itemSKU.ext_field06 = DbData.GetString("EXT_FIELD06");
                            itemSKU.ext_field07 = DbData.GetString("EXT_FIELD07");
                            itemSKU.ext_field08 = DbData.GetString("EXT_FIELD08");
                            itemSKU.ext_field09 = DbData.GetString("EXT_FIELD09");
                            itemSKU.ext_field10 = DbData.GetString("EXT_FIELD10");
                            itemSKU.serial_yn = DbData.GetDecimal("SERIAL_YN");
                            itemSKU.batch_yn = DbData.GetDecimal("BATCH_YN");
                            itemSKU.item_asm_yn = DbData.GetDecimal("ITEM_ASM_YN");
                            itemSKU.point_redeem_yn = DbData.GetDecimal("POINT_REDEEM_YN");
                            itemSKU.matrix_item_yn = DbData.GetDecimal("MATRIX_ITEM_YN");
                            itemSKU.inactive_yn = DbData.GetDecimal("INACTIVE_YN");
                            itemSKU.modfiy_user = DbData.GetString("MODIFY_USER");
                            itemSKU.modify_date = DbData.GetDateTime("MODIFY_DATE");
                            itemSKU.create_user = DbData.GetString("CREATE_USER");
                            itemSKU.create_date = DbData.GetDateTime("CREATE_DATE");
                            itemSKU.modify_flag = DbData.GetString("MODIFY_FLAG");
                            itemSKU.purchaser = DbData.GetString("PURCHASER");
                            itemSKU.pos_pac = DbData.GetDecimal("POS_PAC");
                            itemSKU.feature_1 = DbData.GetString("FEATURE_1");
                            itemSKU.feature_2 = DbData.GetString("FEATURE_2");
                            itemSKU.feature_3 = DbData.GetString("FEATURE_3");
                            itemSKU.feature_4 = DbData.GetString("FEATURE_4");
                            itemSKU.war_and_cond_1 = DbData.GetString("WAR_AND_COND_1");
                            itemSKU.war_and_cond_2 = DbData.GetString("WAR_AND_COND_2");
                            itemSKU.vintage = DbData.GetString("VINTAGE");
                            itemSKU.producer = DbData.GetString("PRODUCER");
                            itemSKU.classification = DbData.GetString("CLASSIFICATION");
                            itemSKU.region = DbData.GetString("REGION");
                            itemSKU.appellation = DbData.GetString("APPELLATION");
                            itemSKU.grape_variety = DbData.GetString("GRAPE_VARIETY");
                            itemSKU.country = DbData.GetString("COUNTRY");
                            itemSKU.size_ml = DbData.GetString("SIZE_ML");
                            itemSKU.item_matrix_name_1 = DbData.GetString("ITEM_MATRIX_NAME_1");
                            itemSKU.item_name_1 = DbData.GetString("ITEM_NAME_1");
                            itemSKU.item_short_name_1 = DbData.GetString("ITEM_SHORT_NAME_1");
                            itemSKU.item_matrix_name_1 = DbData.GetString("ITEM_MATRIX_NAME_2");
                            itemSKU.item_name_2 = DbData.GetString("ITEM_NAME_2");
                            itemSKU.item_short_name_2 = DbData.GetString("ITEM_SHORT_NAME_2");
                            itemSKU.non_disc_item_yn = DbData.GetDecimal("NON_DISC_ITEM_YN");
                            itemSKU.coupon_item_yn = DbData.GetDecimal("COUPON_ITEM_YN");
                            itemSKU.coupon_effective_from = DbData.GetDateTime("COUPON_EFFECTIVE_FROM");
                            itemSKU.coupon_effective_to = DbData.GetDateTime("COUPON_EFFECTIVE_TO");
                            itemSKU.item_cut = DbData.GetString("ITEM_CUT");
                            itemSKU.item_clarity = DbData.GetString("ITEM_CLARITY");
                            itemSKU.online_yn = DbData.GetDecimal("ONLINE_YN");
                            itemSKU.webstock_yn = DbData.GetDecimal("WEBSTOCK_YN");
                            itemSKU.web_price = DbData.GetDecimal("WEB_PRICE");
                            itemSKU.package_yn = DbData.GetDecimal("PACKAGE_YN");
                            itemSKU.accode_01 = DbData.GetString("ACCODE_01");
                            itemSKU.accode_02 = DbData.GetString("ACCODE_02");
                            itemSKU.accode_03 = DbData.GetString("ACCODE_03");
                            itemSKU.accode_04 = DbData.GetString("ACCODE_04");
                            itemSKU.accode_05 = DbData.GetString("ACCODE_05");
                            itemSKU.accode_06 = DbData.GetString("ACCODE_06");
                            itemSKU.accode_07 = DbData.GetString("ACCODE_07");
                            itemSKU.accode_08 = DbData.GetString("ACCODE_08");
                            itemSKU.accode_09 = DbData.GetString("ACCODE_09");
                            itemSKU.accode_10 = DbData.GetString("ACCODE_10");
                            itemSKU.accode_11 = DbData.GetString("ACCODE_11");
                            itemSKU.accode_12 = DbData.GetString("ACCODE_12");
                            itemSKU.accode_13 = DbData.GetString("ACCODE_13");
                            itemSKU.accode_14 = DbData.GetString("ACCODE_14");
                            itemSKU.accode_15 = DbData.GetString("ACCODE_15");




                        }
                    }
                }
                DBConnection.Close();
            }
            
            return itemSKU;
        }
    }
}