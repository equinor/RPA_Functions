using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            List<MaterialDeliveryEntity> materialDeliveriesSorted = materialDeliveries.OrderBy(p => p.po).ToList();
            


            foreach (MaterialDeliveryEntity ent in materialDeliveriesSorted)
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
                                    <td>{ent.delivery_date}</td>
                                    <td>
                                    <input class=delivery type=radio name=delivery_{ent.id} id=delivery_yes_{ent.id} checked value=yes>Yes<br>
                                    <input class=delivery type=radio name=delivery_{ent.id} id=delivery_no_{ent.id} value=no>No<br>
                                    </td>
                                    <td>
                                    <input class=deliverydate name=deliverydate_{ent.id} disabled type=text id=deliverydate_{ent.id}>
                                    </td>
                                    <td>
                                    <input class=trackingnr name=trackingnr_{ent.id} input=text id=trackingnr_{ent.id}>
                                    </td>
                                    <td>
                                    <input class=freight name=freight_{ent.id} input=text id=freight_{ent.id}>
                                    </td>
                                    <td>
                                    <button class=submit id=button_{ent.id} type=button>Submit</button>
                                    </td>
                                    </tr>";

                htmlTable = htmlTable + tableLine;
            }

            return htmlHead + htmlTable + htmltail;
        }

        private static string gethtmlhead(string vendor_name, string webguid)
        {
            //string form_action = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") + MaterialDeliveryConstants.WEB_PATH_POST + webguid;

            string retVal = $@"
                            <html>
                            <title>Equinor Material Delivery feedback</title>
                            <script src=https://code.jquery.com/jquery-3.4.1.js integrity=sha256-WpOohJOqMqqyKL9FccASB9O0KwACQJpFTUBLTYOVvVU= crossorigin=anonymous></script>
                            </head>
                            <body>
                            <h3>{vendor_name}</h3>
                            <input type=hidden id=webid value={webguid}>
                            <br>
                            <table border=1>
                            <tr>
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
                            </tr>";
            return retVal;
        }

        private const string htmltail = @"</table>
                                          <br>
                                          <script>
                                            $('.delivery').click(function() {
                                              var id = this.id.split('_')[2];
                                              var action = this.id.split('_')[1];

                                              if (action == 'yes') {
                                                $('#trackingnr_' + id).prop('disabled', false);
                                                $('#freight_' + id).prop('disabled', false);
                                                $('#deliverydate_' + id).prop('disabled', true);
                                              } else if (action == 'no') {
                                                $('#trackingnr_' + id).prop('disabled', true);
                                                $('#freight_' + id).prop('disabled', true);
                                                $('#deliverydate_' + id).prop('disabled', false);
                                              }
                                            });

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
        public int status { get; set; } = 0;
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
            if (Convert.ToString(bodyData.delivery_date) != "") returnEntity.delivery_date = DateTime.Parse(Convert.ToString(bodyData.delivery_date));

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
            if (Convert.ToString(bodyData.new_delivery_date) != "")  materialDelivery.new_delivery_date = DateTime.Parse(Convert.ToString(bodyData.new_delivery_date));
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
    }
}