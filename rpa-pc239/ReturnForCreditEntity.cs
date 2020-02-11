using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc239
{
    public static class HtmlTemplate
    {
        public static string GetPage(List<ReturnForCreditEntityTableEntity> returnForCreditTable)
        {
            List<ReturnForCreditEntity> returnForCreditData = Mappings.ToReturnForCreditEntityList(returnForCreditTable);

            string htmlHead = gethtmlhead(returnForCreditData[0].webguid);
            string htmlTable = "";

            string formHead = $@"
                            <h3>Goods return form</h3>
                            <table border=1>
                                <tr>
                                    <td><label>Return Number</label></td>
                                    <td><input type=text disabled=disabled class=returnNo value={returnForCreditData[0].returns_number} /></td>
                                </tr>
                                <tr>
                                    <td><label>PO Responsible Name</label></td>
                                    <td><input type=text disabled=disabled class=poresponse value={returnForCreditData[0].PO_responsible_name} /></td>
                                </tr>";
             string formTail = $@"                  
                                <tr>
                                    <td><label>Credit Amount</label></td>
                                    <td><input type=text class=credit value={returnForCreditData[0].credit_amount} /></td>
                                </tr>
                                <tr>
                                    <td><label>Contact Person</label></td>
                                    <td><input type=text class=contact value={returnForCreditData[0].contact_person} /></td>
                                </tr>
                                <tr>
                                    <td><label>Address</label></td>
                                    <td><input type=text class=address value={returnForCreditData[0].address} /></td>
                                </tr>
                                <tr>
                                    <td><label>City</label></td>
                                    <td><input type=text class=city value={returnForCreditData[0].city} /></td>
                                </tr>
                                <tr>
                                    <td><label>Postal Code</label></td>
                                    <td><input type=text class=postCode value={returnForCreditData[0].zip_code} /></td>
                                </tr>
                                <tr>
                                    <td><label>Comments</label></td>
                                    <td><input type=text class=comments value={returnForCreditData[0].comments} /></td>
                                </tr>
                                <tr>
                                    <td><label>Reason for Return</label></td>
                                    <td><input type=text disabled=disabled class=reason value={returnForCreditData[0].reason} /></td>
                                </tr>
                                <tr>
                                    <td><label>Condition of Goods</label></td>
                                    <td><input type=text disabled=disabled class=condition value={returnForCreditData[0].condition} /></td>
                                </tr>
                                <tr>
                                    <td><label>Transportation Cost Covered By</label></td>
                                    <td><input type=text disabled=disabled class=transportCover value={returnForCreditData[0].transportation_cost_cover_by} /></td>
                                </tr>
                            </table>
                            <button class=submit type=button>Submit</button>";

            string formBody = getPurchaseDocumentTable(returnForCreditData);

            htmlTable = htmlTable + formHead + formBody + formTail;
            return htmlHead + htmlTable + htmlTail;
        }

        private static string gethtmlhead(string webguid)
        {
            string retVal = $@"
                            <html>
                            <head>
                            <title>Equinor Return for Credit Form</title>
                            <script src=https://code.jquery.com/jquery-3.4.1.js integrity=sha256-WpOohJOqMqqyKL9FccASB9O0KwACQJpFTUBLTYOVvVU= crossorigin=anonymous></script>
                            </head>
                            <body>
                            <h3></h3>
                            <input type=hidden id=webid value={webguid}>
                            <br>";
            return retVal;
        }

        private static string getPurchaseDocumentTable(List<ReturnForCreditEntity> returnForCreditEntityList)
        {
            string purchaseTableHead = @"<table border=1>
                                    <tr>
                                    <th>Purchase Document</th>
                                    <th>Item</th>
                                    <th>Text</th>
                                    <th>Quantity</th>
                                    <th>Total</th>
                                    </tr>
                              ";

            string purchaseTableTail = "</table>";
            string purchaseTableBody = "";
            foreach(var ent in returnForCreditEntityList)
            {
                purchaseTableBody += $@"<tr>
                                    <td><input type=text disabled=disabled class=purchaseDoc value={ent.purchase_document} /></td>
                                    <td><input type=text disabled=disabled class=item value={ent.item} /></td>
                                    <td><input type=text disabled=disabled class=text value={ent.text} /></td>

                                    <td><input type=text disabled=disabled class=quantity value={ent.quantity} /></td>
                                    <td><input type=text disabled=disabled class=EUn value={ent.total} /></td>
                                    </tr>";
            }

            return purchaseTableHead + purchaseTableBody + purchaseTableTail;
        }

        private const string htmlTail = @"
                                        <br>
                                          <script>
                                            $('.submit').click(function() {
                                              var id = this.id.split('_')[1];
                                              var dateRegExp = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/

                                              //alert(dateRegExp.test('22/01/1981'));
                                              if ($('#delivery_no_' + id).prop('checked')) {
                                                var deliveryDate = $('#deliverydate_' + id).val();

                                                if (dateRegExp.test(deliveryDate)) {
                                                  // Send with date
                                                  $('#deliverydate_' + id).css({
                                                    'color': 'black'
                                                  });
                                                  $('#button_' + id).prop('disabled', true);
                                                } else {
                                                  // Not valid date, make user correct it
                                                  $('#deliverydate_' + id).css({
                                                    'color': 'red'
                                                  });
                                                  alert('date not valid - NO SEND');
                                                }
                                              } else {
                                                // Send without date
                                                $('#deliverydate_' + id).val('');
                                                $('#button_' + id).prop('disabled', true);
                                                makehttp(id, true);
                                              }
                                            });

                                            var apiurl = 'http://localhost:7071/api/PC243_PostMaterialDeliveryUpdate/';

                                            var materialDeliveryData = {
                                              'delivered_ondate': '',
                                              'new_delivery_date': '',
                                              'tracking_nr': '',
                                              'freight_name': ''
                                            };

                                            function makehttp(id, ontime) {
                                            var webid = $('#webid').val();
                                            var guid = id;

                                            var urlmaterial = apiurl + webid + '/' + guid;

                                              if (ontime) {
                                                materialDeliveryData.delivered_ondate = '1';
                                              } else {
                                                materialDeliveryData.delivered_ondate = '0';
                                              }

                                              materialDeliveryData.new_delivery_date = $('#deliverydate_' + id).val();
                                              materialDeliveryData.tracking_nr = $('#trackingnr_' + id).val();
                                              materialDeliveryData.freight_name = $('#freight_' + id).val();


                                              $.ajax({
                                                url: urlmaterial,
                                                data: JSON.stringify(materialDeliveryData), //ur data to be sent to server
                                                contentType: 'application/json',
                                                dataType: 'json',
                                                type: 'PATCH',
                                                success: function(data) {
                                                },
                                                error: function(xhr, ajaxOptions, thrownError) {
                                                  alert('Failed:  '+ thrownError+xhr.responseText + '   ' + xhr.status);
                                                }
                                              });
                                            }

                                          </script>
                                          </body>
                                          </html>";
    }
    


    public class ReturnForCreditEntityConstants
    {
        public const string ID_FIELD_NAME = "RowKey";
        public const string WEBID_FIELD_NAME = "PartitionKey";

        public const string STATUS_FIELD_NAME = "status";
        public const int STATUS_WAITING_PO_RESPONSE = 0;
        public const int STATUS_WAITING_EXTERNAL = 1;
        public const int STATUS_DONE = 2;
        public const int STATUS_FETCHED = 3;
        public const int STATUS_EXPIRED = 4;
        public const string RETURNS_NUMBER_FIELD_NAME = "Returns Number";
    }

    public class ReturnForCreditEntity
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string webguid { get; set; }
        public string returns_number { get; set; }
        public string PO_responsible_name { get; set; }
        public string purchase_document { get; set; }
        public string item { get; set; }
        public string text { get; set; }
        public string quantity { get; set; }
        public string total { get; set; }
        public string credit_amount { get; set; }
        public string contact_person { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string comments { get; set; }
        public string reason { get; set; }
        public string condition { get; set; }
        public string transportation_cost_cover_by { get; set; }
        public int status { get; set; } = ReturnForCreditEntityConstants.STATUS_WAITING_PO_RESPONSE;
    }

    public class ReturnForCreditEntityTableEntity : TableEntity
    {
        public string returns_number { get; set; }
        public string PO_responsible_name { get; set; }
        public string purchase_document { get; set; }
        public string item { get; set; }
        public string text { get; set; }
        public string quantity { get; set; }
        public string total { get; set; }
        public string credit_amount { get; set; }
        public string contact_person { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string comments { get; set; }
        public string reason { get; set; }
        public string condition { get; set; }
        public string transportation_cost_cover_by { get; set; }
        public int status { get; set; }
    }

    public static class Mappings
    {
        public static ReturnForCreditEntityTableEntity ToReturnForCreditEntityTableEntity(this ReturnForCreditEntity returnForCreditEntry)
        {
            return new ReturnForCreditEntityTableEntity()
            {
                PartitionKey = returnForCreditEntry.webguid,
                RowKey = returnForCreditEntry.id,
                returns_number = returnForCreditEntry.returns_number,
                PO_responsible_name = returnForCreditEntry.PO_responsible_name,
                purchase_document = returnForCreditEntry.purchase_document,
                item = returnForCreditEntry.item,
                text = returnForCreditEntry.text,
                quantity = returnForCreditEntry.quantity,
                total = returnForCreditEntry.total,
                credit_amount = returnForCreditEntry.credit_amount,
                contact_person = returnForCreditEntry.contact_person,
                address = returnForCreditEntry.address,
                city = returnForCreditEntry.city,
                zip_code = returnForCreditEntry.zip_code,
                reason = returnForCreditEntry.reason,
                condition = returnForCreditEntry.condition,
                transportation_cost_cover_by = returnForCreditEntry.transportation_cost_cover_by,
                status = returnForCreditEntry.status
            };
        }

        public static ReturnForCreditEntity ToReturnForCreditEntity(this ReturnForCreditEntityTableEntity returnGoodsTableEntity)
        {
            return new ReturnForCreditEntity()
            {
                webguid = returnGoodsTableEntity.PartitionKey,
                id = returnGoodsTableEntity.RowKey,
                returns_number = returnGoodsTableEntity.returns_number,
                PO_responsible_name = returnGoodsTableEntity.PO_responsible_name,
                purchase_document = returnGoodsTableEntity.purchase_document,
                item = returnGoodsTableEntity.item,
                text = returnGoodsTableEntity.text,
                quantity = returnGoodsTableEntity.quantity,
                total = returnGoodsTableEntity.total,
                credit_amount = returnGoodsTableEntity.credit_amount,
                contact_person = returnGoodsTableEntity.contact_person,
                address = returnGoodsTableEntity.address,
                city = returnGoodsTableEntity.city,
                zip_code = returnGoodsTableEntity.zip_code,
                comments = returnGoodsTableEntity.comments,
                reason = returnGoodsTableEntity.reason,
                condition = returnGoodsTableEntity.condition,
                transportation_cost_cover_by = returnGoodsTableEntity.transportation_cost_cover_by,
                status = returnGoodsTableEntity.status
            };
        }

        public static List<ReturnForCreditEntity> ToReturnForCreditEntityList(List<ReturnForCreditEntityTableEntity> returnForCreditEntityTableEntityList)
        {
            var tmpReturnForCreditEntityList = new List<ReturnForCreditEntity>();

            foreach(var ent in returnForCreditEntityTableEntityList)
            {
                tmpReturnForCreditEntityList.Add(ToReturnForCreditEntity(ent));
            }

            return tmpReturnForCreditEntityList;
        }

        public static List<ReturnForCreditEntityTableEntity> ToReturnForCreditEntityTableEntityList(List<ReturnForCreditEntity> returnForCreditEntityList)
        {
            var tmpReturnForCreditEntityTableEntityList = new List<ReturnForCreditEntityTableEntity>();

            foreach(var ent in returnForCreditEntityList)
            {
                tmpReturnForCreditEntityTableEntityList.Add(ToReturnForCreditEntityTableEntity(ent));
            }

            return tmpReturnForCreditEntityTableEntityList;
        }

        public static ReturnForCreditEntityTableEntity updateReturnForCreditEntityTableEntity(ReturnForCreditEntityTableEntity returnForCredit, dynamic bodyData)
        {
            returnForCredit.credit_amount = bodyData.credit_amount;
            returnForCredit.contact_person = bodyData.contact_person;
            returnForCredit.address = bodyData.address;
            returnForCredit.city = bodyData.city;
            returnForCredit.zip_code = bodyData.zip_code;
            returnForCredit.comments = bodyData.comments;

            //define as done
            returnForCredit.status = ReturnForCreditEntityConstants.STATUS_DONE;
            return returnForCredit;
        }
    }
}
