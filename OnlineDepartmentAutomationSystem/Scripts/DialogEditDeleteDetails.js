    $(document).ready(function () {





            $(".hi").hide();
            var url = "";

            if ('@TempData["msg"]' != "") {
                $("#dialog-alert").dialog('open');
            }
            $("#dialog-edit").dialog({
                title: 'Create User',
                autoOpen: false,
                resizable: false,
                width: $(window).width() - 250,
                position: [30, 20],
                show: { effect: 'drop', direction: "up" },
                modal: true,
                draggable: false,
                open: function (event, ui) {
                    $(".ui-dialog-titlebar-close").hide();
                    $(this).load(url);
                }
            });

            $("#dialog-confirm").dialog({
                autoOpen: false,
                title: ' <span style="color:#fff"> Delete Pannel</span>',
                resizable: false,
             
                width: 500,
                position: [370, 120],
                show: { effect: 'drop', direction: "up" },
                modal: true,
                draggable: true,
                open: function (event, ui) {
                    $(".ui-dialog-titlebar-close").hide();

                },
                buttons: {
                    "Yes": function () {
                        $(this).dialog("close");
                        lodingFunction();
                        window.location.href = url;
                    },
                    "No": function () {
                        $(this).dialog("close");
                    }
                }
            });

            $("#dialog-detail").dialog({
                title: '  <img src="/Content/info_blue.png" width="34"/> <span style="font-family:Courier New;color:white">Student Details</span>',
                autoOpen: false,
                resizable: false,
                width:$(window).width()-150,
                position: [30, 20],
                show: { effect: 'drop', direction: "up" },
                modal: true,
                draggable: false,
                open: function (event, ui) {
                    $(".ui-dialog-titlebar-close").hide();
                    $(this).load(url);
                },
                buttons: {
                    "Close": function () {
                        $(this).dialog("close");
                    }
                }
            });



            $(".lnkDelete").live("click", function (e) {
                // e.preventDefault(); use this or return false

                url = $(this).attr('href');
                $("#dialog-confirm").dialog('open');

                return false;
            });

            $(".lnkEdit").live("click", function (e) {
                // e.preventDefault(); use this or return false
                url = $(this).attr('href');
                $(".ui-dialog-title").html('<img src="/Content/system_software_update.png" width="35"/><span style="color:#fff"> Update</span>  ');
                $("#dialog-edit").dialog('open');

                return false;
            });
            $(".lnkDetail").live("click", function (e) {
                // e.preventDefault(); use this or return false
                url = $(this).attr('href');
                $("#dialog-detail").dialog('open');

                return false;
            });

            $("#btnClose").live("click", function (e) {
                $("#dialog-edit").dialog("close");
                return false;
            });
        });