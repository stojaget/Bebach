﻿$(function () {
    var vrstaAkt = $.ajax(
        {
            url: '/Aktivnost/GetVrsteAkt', async: false,
            success: function (data, result) {
                if (!result) {
                    alert('Greška prilikom dohvata vrste aktivnosti');
                }
            }
        }
        ).responseText;
    $("#grid").jqGrid({
        url: "/Aktivnost/GetAktLists",
        datatype: 'json',
        mtype: 'Get',
        //  ID,Datum,Opis,TrajanjeOd,TrajanjeDo,BebaID,VrstaID,Cijena
        colNames: ['Id', 'BebaID', 'Datum', 'Opis', 'Od', 'Do', 'Vrsta', 'Cijena'],
        colModel: [
            { key: true, hidden: true, name: 'ID', index: 'ID', editable: false },
            { key: false, hidden: true, name: 'BebaID', index: 'BebaID', editable: false },
            { key: false, name: 'Datum', index: 'Datum', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
             { key: false, name: 'Opis', index: 'Opis', editable: true },
             { key: false, name: 'TrajanjeOd', index: 'TrajanjeOd', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
              { key: false, name: 'TrajanjeDo', index: 'TrajanjeDo', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
           {
               key: false, name: 'Vrsta', index: 'Vrsta', editable: true, edittype: "select", editrules: { required: true }, editoptions: {
                   size: "20",
                   maxlength: "30",
                   dataUrl: "/Aktivnost/GetVrsteAkt",
                   buildSelect: function (data) {
                       var response = jQuery.parseJSON(data.responseText);
                       var s = '<select>';
                       if (response && response.length) {
                           for (var i = 0, l = response.length; i < l ; i++) {
                               var ri = response[i];
                               s += '<option value="' + ri + '">' + ri + '</option>';
                           }
                       }
                       return s + "</select>";
                   }
               }
           }
           ,
           { key: false, name: 'Cijena', index: 'Cijena', editable: true, formatter: 'currency', formatoptions: { decimalSeparator: ".", decimalPlaces: 2, prefix: 'KN' } }],
        pager: jQuery('#pager'),
        rowNum: 10,
        //loadComplete: function () {
        //    $("#grid").setColProp('Vrsta', { editoptions: { value: vrstaAkt } });
        //},
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Aktivnosti',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/Aktivnost/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/Aktivnost/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/Aktivnost/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Da li želite obrisati aktivnost?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});