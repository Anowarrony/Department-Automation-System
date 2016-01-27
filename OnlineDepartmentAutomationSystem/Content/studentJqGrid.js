function showGrid() {
    $('#_studentBacklokGrid').jqGrid({
        caption: paramFromView.Caption,
        colNames: ['ID', paramFromView.VisitorName, paramFromView.StartDate, 
          paramFromView.EndDate, paramFromView.WasTimeOut, paramFromView.Actions],
        colModel: [
                    { name: 'ID', width: 1, hidden: true, key: true },
                    { name: 'VisitorName', index: 'User.Username', width: 300 },
                    { name: 'StartDate', index: 'StartDate', width: 150 },
                    { name: 'EndDate', index: 'EndDate', width: 150 },
                    { name: 'WasTimeOut', index: 'WasTimeOut', width: 120, 
                            formatter: "checkbox", align: "center" },
                    { name: 'Action', index: 'ID', width: 70, align: "center" }
                  ],
        hidegrid: false,
        pager: jQuery('#_studentLogPager'),
        sortname: 'ID',
        rowNum: paramFromView.PageSize,
        rowList: [10, 20, 50, 100],
        sortorder: "desc",
        width: paramFromView.Width,
        height: paramFromView.Height,
        datatype: 'json',
        caption: paramFromView.Caption,
        viewrecords: true,
        mtype: 'GET',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            userdata: "userdata"
        },
        url: paramFromView.Url
    }).navGrid('#_studentLogPager', { view: false, del: false, add: false, edit: false, search: false },
       { width: 400 }, // default settings for edit
       {}, // default settings for add
       {}, // delete instead that del:false we need this
       {}, // search options
       {} /* view parameters*/
     ).navButtonAdd('#_visitorLogPager', {
         caption: paramFromView.DeleteAllCaption, buttonimg: "", onClickButton: function () {
             if (confirm(paramFromView.DeleteAllConfirmationMessage)) {
                 document.location = paramFromView.ClearGridUrl;
             }
             else {
                 $('#_studentBacklokGrid').resetSelection();
             }
         }, position: "last"
     });
};

$(document).ready(function () {
    showGrid();
});