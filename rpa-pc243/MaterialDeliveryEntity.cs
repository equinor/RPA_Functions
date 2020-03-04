using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace rpa_functions.rpa_pc243
{
    public static class HtmlTemplate
    {
        public static string GetPage(List<MaterialDeliveryTableEntity> materialDeliveriesTable)
        {
            List<MaterialDeliveryEntity> materialDeliveries = Mappings.toMaterialDeliveryEntityList(materialDeliveriesTable);

            string htmlHead = gethtmlhead(materialDeliveries[0].vendor_name, materialDeliveries[0].webguid);
            string htmlTable = "";

            foreach (MaterialDeliveryEntity ent in materialDeliveries)
            {
                string tableLine = $@"
                                    <tr>
                                    <td>{ent.vendor_number}</td>
                                    <td>{ent.vendor_name}</td>
                                    <td>{ent.po}</td>
                                    <td>{ent.item}</td>
                                    <td>{ent.material}</td>
                                    <td>{ent.shorttext}</td>
                                    <td>{ent.order_qty}</td>
                                    <td>{ent.order_unit}</td>
                                    <td>{Convert.ToString(ent.delivery_date).Substring(0, Convert.ToString(ent.delivery_date).IndexOf(" "))}</td>
                                    <td class=checkboxes>
                                    <label class = container>Yes
                                        <input class=delivery type=radio name=delivery_{ent.id} id=delivery_yes_{ent.id} checked value=yes><br>
                                        <span class = checkmark></span>
                                    </label>
                                    <label class = container>No
                                        <input class=delivery type=radio name=delivery_{ent.id} id=delivery_no_{ent.id} value=no><br>
                                        <span class = checkmark></span>
                                    </label>
                                    </td>
                                    <td>
                                    <input class=deliverydate name=deliverydate_{ent.id} type=date id=deliverydate_{ent.id} disabled=true>
                                    </td>
                                    <td>
                                    <input class=trackingnr name=trackingnr_{ent.id} input=text id=trackingnr_{ent.id} placeholder = ""Type the value"">
                                    </td>
                                    <td>
                                    <input class=freight name=freight_{ent.id} input=text id=freight_{ent.id} placeholder = ""Type the value"">
                                    </td>
                                    <td>
                                    <button class=submit id=button_{ent.id} type=button>Submit</button>
                                    </td>
                                    </tr>
                                    ";



                htmlTable = htmlTable + tableLine;
            }

            return htmlHead + htmlTable + htmltail;
        }

        private static string gethtmlhead(string vendor_name, string webguid)
        {
            //string form_action = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") + MaterialDeliveryConstants.WEB_PATH_POST + webguid;

            string retVal = $@"
                            <html>
                            <head>
                            <title>Equinor Material Delivery feedback</title> 
                            <script src=https://code.jquery.com/jquery-3.4.1.js integrity=sha256-WpOohJOqMqqyKL9FccASB9O0KwACQJpFTUBLTYOVvVU= crossorigin=anonymous></script>
                            <style>
                                
                                h3, p {{
                                    padding: 3px 6px;
                                    color: #1C6EA4;
                                }}
                                table {{
                                    border: 1px solid #1852AE;
                                    background-color: #EEEEEE;
                                    width: 100%;
                                    text-align: left; 
                                    table-layout: fixed;
                                }}
                                
                                td, th {{
                                    border: 1px solid #AAAAAA;
                                    padding: 3px 2px;
                                }}
                                td {{
                                    word-wrap: break-word;
                                    font - size: 13px;
                                    padding-left: 5px;
                                }}
                                
                                .checkboxes {{
                                    padding-top: 10px;
                                }}
                                thead {{
                                    background: #1C6EA4;
                                    background: -moz-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
                                    background: -webkit-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
                                    background: linear-gradient(to bottom, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
                                    border-bottom: 2px solid #444444;
                                }}
    
                                thead th {{
                                    font-size: 15px;
                                    font-weight: bold;
                                    color: #FFFFFF;
                                    text-align: center;
                                    border-left: 2px solid #D0E4F5;
                                }}
                                
                                thead th:first-child {{
                                    border-left: none;
                                }}
                            
                                tfoot td {{
                                    font-size: 14px;
                                }}
                                input {{
                                    border: none;
                                    background-color: transparent;
                                    width: 100%;
                                }}
                                
                                .submit {{
                                    margin: auto;
                                    display: block;
                                    border-radius: 10%;
                                    font-weight: bold;
                                    background: #1C6EA4;
                                    color: #FFFFFF;
                                    padding: 6px 16px;
                                    border: 2px solid #1C6EA4;
                                }}
                                .submit:hover {{
                                    background-color: #D0E4F5;
                                    border: 2px solid #D0E4F5;
                                }}
                    
                                .container {{
                                      display: block;
                                      position: relative;
                                      padding-left: 25px;
                                      margin-bottom: 12px;
                                      cursor: pointer;
                                      font-size: 16px;
                                      -webkit-user-select: none;
                                      -moz-user-select: none;
                                      -ms-user-select: none;
                                      user-select: none;
                                }}
                                .container input {{
                                      padding-top: 10px;
                                      position: absolute;
                                      opacity: 0;
                                      cursor: pointer;
                                      height: 0;
                                      width: 0;
                                }}
                                .checkmark {{
                                      position: absolute;
                                      top: 0;
                                      left: 0;
                                      height: 18px;
                                      width: 18px;
                                      background-color: #D0E4F5;
                                      border-radius: 50%;
                                }}
                                .container:hover input ~ .checkmark {{
                                      background - color: #ccc;
                                }}
                                .container input:checked ~ .checkmark {{
                                    background-color: #1C6EA4;
                                }}
                                .checkmark:after {{
                                      content: "";
                                      width: 10px;
                                      height: 10px;
                                      position: absolute;
                                      top: 5px;
                                      left: 5px;
                                      display: none;
                                }}
                                .container input:checked ~ .checkmark:after {{
                                      display: block;
                                }}
                                
                                .buttonsubmitall {{
                                    float: right;
                                    padding-right: 2px;
                                    margin: auto;
                                    display: block;
                                    width: auto;
                                    border-radius: 10%;
                                    font-weight: bold;
                                    background: #1C6EA4;
                                    color: #FFFFFF;
                                    padding: 6px 16px;
                                    border: 2px solid #1C6EA4;
                                }} 
                                .buttonsubmitall:hover {{
                                    background-color: #D0E4F5;
                                    border: 2px solid #D0E4F5;
                                }}
                                ::placeholder {{
                                    text-align: center;
                                    
                                }}
                            </style>
                            </head>
                            <body>
                            <h3>{vendor_name}</h3>
                            <input type=hidden id=webid value={webguid}>
                            <br>
                            <p>Please update the list and press Submit All at the bottom or update items and Submit individually</p>
                            <br>
                            <table>
                            <tr>
                            <thead>
                            <th>Vendor No</th>
                            <th>Vendor Name</th>
                            <th>PO</th>
                            <th>Item</th>
                            <th>Material</th>
                            <th>Short text</th>
                            <th>Order qty</th>
                            <th>Order unit</th>
                            <th>Delivery date</th>
                            <th>Deliver on agreed date?</th>
                            <th>New delivery date</th>
                            <th>Tracking nr</th>
                            <th>Freight forwarder</th>
                            <th>Submit</th>
                            </thead>
                            </tr>
                            ";
            return retVal;
        }

        private const string htmltail = @"</table>
                                          <br>
                                          <div class=submitallbtn>
                                            <button class=buttonsubmitall id=buttonsubmitall type=button>Submit All</button>
                                          </div>
                                          <script>
                                            $('.delivery').click(function() {
                                              var id = this.id.split('_')[2];
                                              var action = this.id.split('_')[1];
                                              if (action == 'yes') {
                                                $('#trackingnr_' + id).prop('disabled', false);
                                                $('#freight_' + id).prop('disabled', false);
                                                $('#deliverydate_' + id).prop('disabled', true);
                                              } else if (action == 'no') {
                                                $('#trackingnr_' + id).prop('disabled', false);
                                                $('#freight_' + id).prop('disabled', false);
                                                $('#deliverydate_' + id).prop('disabled', false);
                                              }
                                            });
                                            $('#buttonsubmitall').click(function() {
                
                                                var tableRows = $('table')[0].rows;
                                                //var dateRegExp = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/
                                                var dateRegExp = /^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d|([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))$/
                                                for (i = 2; i < tableRows.length; i++) {
                                                    var id = tableRows[i].cells[13].childNodes[1].id.split('_')[1];
                                                    //Disable Each Submit Button
                                                        disableSubmit(id);
                                                    if ($('#delivery_no_' + id).prop('checked')) {
                                                        var deliveryDate = $('#deliverydate_' + id).val();
                                                        if (dateRegExp.test(deliveryDate)) {
                                                          // Send with date
                                                          $('#deliverydate_' + id).css({
                                                            'color': 'black'
                                                          });
                                                          disableSubmitAll();
                                                          makehttp(id, true);
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
                                                        disableSubmitAll();
                                                        makehttp(id, true);
                                                    }
                                                    
                                                 }
                                            });
                                                
                                            $('.submit').click(function() {
                                            // Disable Submit All button
                                              disableSubmitAll();
                                              var id = this.id.split('_')[1];
                                              //var dateRegExp = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/
                                              var dateRegExp = /^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d|([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))$/
                                              //alert(dateRegExp.test('22/01/1981'));
                                              if ($('#delivery_no_' + id).prop('checked')) {
                                                var deliveryDate = $('#deliverydate_' + id).val();
                                                
                                                if (dateRegExp.test(deliveryDate)) {
                                                  // Send with date
                                                  $('#deliverydate_' + id).css({
                                                    'color': 'black'
                                                  });
                                                  disableSubmit(id);
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
                                                
                                                disableSubmit(id);
                                                makehttp(id, true);
                                              }
                                            });
                                            function disableSubmit (id) {
                                                $('#button_' + id).prop('disabled', true);
                                                $('#button_' + id).css({
                                                            'background': '#D0E4F5'
                                                          });
                                                $('#button_' + id).css({
                                                            'border': 'none'
                                                          });
                                            }
                                            function disableSubmitAll () {
                                                $('#buttonsubmitall').prop('disabled', true);
                                                          $('#buttonsubmitall').css({
                                                            'background': '#D0E4F5'
                                                          });
                                                          
                                                          $('#buttonsubmitall').css({
                                                            'border': 'none'
                                                          });
                                            }
                                            var apiurl = 'http://localhost:7071/api/PC243_PostMaterialDeliveryUpdate/';
                                            var materialDeliveryData = {
                                              'delivered_ondate': '',
                                              'new_delivery_date': '',
                                              'tracking_nr': '',
                                              'freight_name': ''
                                            };
                                            function makehttp (id, ontime) {
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
                                              console.log(materialDeliveryData);
                                             
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


    public static class MaterialDeliveryConstants
    {
        public const string ID_FIELD_NAME = "RowKey";
        public const string WEBID_FIELD_NAME = "PartitionKey";

        public const string STATUS_FIELD_NAME = "status";
        public const int STATUS_WAITING = 0;
        public const int STATUS_DONE = 1;
        public const int STATUS_FETCHED = 2;
        public const int STATUS_EXPIRED = 3;

    }

    public class MaterialDeliveryEntity
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string webguid { get; set; }
        public string vendor_number { get; set; }
        public string vendor_name { get; set; }
        public string po { get; set; }
        public string item { get; set; }
        public string material { get; set; }
        public string shorttext { get; set; }
        public string order_qty { get; set; }
        public string order_unit { get; set; }
        public DateTime delivery_date { get; set; } 
        public string delivered_ondate { get; set; }
        public DateTime? new_delivery_date { get; set; }
        public string tracking_nr { get; set; }
        public string freight_name { get; set; }
        public int status { get; set; } = MaterialDeliveryConstants.STATUS_WAITING;
    }

    public class MaterialDeliveryTableEntity : TableEntity
    {
        public string vendor_number { get; set; }
        public string vendor_name { get; set; }
        public string po { get; set; }
        public string item { get; set; }
        public string material { get; set; }
        public string shorttext { get; set; }
        public string order_qty { get; set; }
        public string order_unit { get; set; }
        public DateTime delivery_date { get; set; }
        public string delivered_ondate { get; set; }
        public DateTime? new_delivery_date { get; set; }
        public string tracking_nr { get; set; }
        public string freight_name { get; set; }
        public int status { get; set; }
    }

    public static class Mappings
    {
        public static MaterialDeliveryTableEntity ToMaterialDeliveryTableEntity(this MaterialDeliveryEntity materialDeliveryEntry)
        {
            return new MaterialDeliveryTableEntity()
            {
                PartitionKey = materialDeliveryEntry.webguid,
                RowKey = materialDeliveryEntry.id,
                vendor_number = materialDeliveryEntry.vendor_number,
                vendor_name = materialDeliveryEntry.vendor_name,
                po = materialDeliveryEntry.po,
                item = materialDeliveryEntry.item,
                material = materialDeliveryEntry.material,
                shorttext = materialDeliveryEntry.shorttext,
                order_qty = materialDeliveryEntry.order_qty,
                order_unit = materialDeliveryEntry.order_unit,
                delivery_date = materialDeliveryEntry.delivery_date,
                delivered_ondate = materialDeliveryEntry.delivered_ondate,
                new_delivery_date = materialDeliveryEntry.new_delivery_date,
                tracking_nr = materialDeliveryEntry.tracking_nr,
                freight_name = materialDeliveryEntry.freight_name,
                status = materialDeliveryEntry.status
            };
        }

        public static MaterialDeliveryEntity ToMaterialDeliveryEntity(this MaterialDeliveryTableEntity packageCheckTableEntity)
        {
            return new MaterialDeliveryEntity()
            {
                webguid = packageCheckTableEntity.PartitionKey,
                id = packageCheckTableEntity.RowKey,
                vendor_number = packageCheckTableEntity.vendor_number,
                vendor_name = packageCheckTableEntity.vendor_name,
                po = packageCheckTableEntity.po,
                item = packageCheckTableEntity.item,
                material = packageCheckTableEntity.material,
                shorttext = packageCheckTableEntity.shorttext,
                order_qty = packageCheckTableEntity.order_qty,
                order_unit = packageCheckTableEntity.order_unit,
                delivery_date = packageCheckTableEntity.delivery_date,
                delivered_ondate = packageCheckTableEntity.delivered_ondate,
                new_delivery_date = packageCheckTableEntity.new_delivery_date,
                tracking_nr = packageCheckTableEntity.tracking_nr,
                freight_name = packageCheckTableEntity.freight_name,
                status = packageCheckTableEntity.status
            };
        }

        public static List<MaterialDeliveryEntity> toMaterialDeliveryEntityList(List<MaterialDeliveryTableEntity> materialDeliveryTableEntityList)
        {
            var tmpMaterialDeliveryEntities = new List<MaterialDeliveryEntity>();

            foreach (MaterialDeliveryTableEntity ent in materialDeliveryTableEntityList)
            {
                tmpMaterialDeliveryEntities.Add(ToMaterialDeliveryEntity(ent));
            }

            return tmpMaterialDeliveryEntities;
        }

        public static List<MaterialDeliveryEntity> getMaterialDeliveryEntities(dynamic bodyData)
        {
            List<MaterialDeliveryEntity> MaterialDelivieres = new List<MaterialDeliveryEntity>();

            string newWebGuid = Guid.NewGuid().ToString();

            foreach (var element in bodyData)
            {
                MaterialDeliveryEntity tmpEnt = castInsertDeliveryFromBodyData(element);

                tmpEnt.webguid = newWebGuid;

                MaterialDelivieres.Add(tmpEnt);
            }

            return MaterialDelivieres;
        }

        private static MaterialDeliveryEntity castInsertDeliveryFromBodyData(dynamic bodyData)
        {
            MaterialDeliveryEntity returnEntity = new MaterialDeliveryEntity();

            returnEntity.vendor_number = bodyData.vendor_number;
            returnEntity.vendor_name = bodyData.vendor_name;
            returnEntity.po = bodyData.po;
            returnEntity.item = bodyData.item;
            returnEntity.material = bodyData.material;
            returnEntity.shorttext = bodyData.shorttext;
            returnEntity.order_qty = bodyData.order_qty;
            returnEntity.order_unit = bodyData.order_unit;
            returnEntity.delivery_date = ConvertToDate(bodyData.delivery_date);

            return returnEntity;

        }

        // FIX THIS::::
        private static MaterialDeliveryEntity castFromBodyData(dynamic bodyData)
        {
            MaterialDeliveryEntity returnEntity = new MaterialDeliveryEntity();


            returnEntity.delivered_ondate = bodyData.delivered_ondate;
            if (Convert.ToString(bodyData.new_delivery_date) != "") returnEntity.new_delivery_date = DateTime.Parse(Convert.ToString(bodyData.new_delivery_date));
            returnEntity.tracking_nr = bodyData.tracking_nr;
            returnEntity.freight_name = bodyData.freight_name;

            return returnEntity;
        }

        public static MaterialDeliveryTableEntity updateMaterialDelivery(MaterialDeliveryTableEntity materialDelivery, dynamic bodyData)
        {

            materialDelivery.delivered_ondate = bodyData.delivered_ondate;
            materialDelivery.new_delivery_date = ConvertToDate(bodyData.new_delivery_date);
            materialDelivery.tracking_nr = bodyData.tracking_nr;
            materialDelivery.freight_name = bodyData.freight_name;

            // Define as done
            materialDelivery.status = MaterialDeliveryConstants.STATUS_DONE;

            return materialDelivery;
        }

    

        // Convert List of MaterialDeliveryTableEntities to JSON
        public static string toMaterialDeliveryJSON(List<MaterialDeliveryTableEntity> materialDeliveryEntities)
        {
            var tmpMaterialDeliveryEntities = new List<MaterialDeliveryEntity>();

            foreach (MaterialDeliveryTableEntity ent in materialDeliveryEntities)
            {
                //tmpMaterialDeliveryEntities.Add(ToPackageCheckEntity(ent));
            }

            return JsonConvert.SerializeObject(tmpMaterialDeliveryEntities);
        }



        private static DateTime? ConvertToDate(dynamic dateDynamic)
        {
            DateTime outputDate;
            CultureInfo MyCultureInfo = new CultureInfo("no-NO");

            if (DateTime.TryParse(Convert.ToString(dateDynamic), out outputDate)) {

                outputDate = new DateTime(outputDate.Year, outputDate.Month, outputDate.Day, 5,0,0);
                return outputDate;
            }

            return null; // default return value if missing or invalid type

        }




    }
}