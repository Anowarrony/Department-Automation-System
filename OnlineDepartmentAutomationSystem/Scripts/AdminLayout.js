
            $(".navigation__inner li a").hover(function () {

                $(this).css('color', 'white').css('background', 'green').css('text-decoration', 'none');
            }, function () {

                $(this).css('color', 'white').css('background', '#212121');
            });

            $("#heloId").css('color', 'white');
            $("#excpFoLayoutPag").dialog({ buttons: { "close": function () { $(this).dialog('close') } } });

            function lodingFunction() {
                $('#divLoading').dialog({ title: '<span style="font-family:Courier New;"> Please Wait..</span>' });

            }

            $("#logOffLink").click(function () {
                lodingFunction();
            });

            function LoadUrl(parameters) {
                history.go(-1);
            }
            $('#MainPageLinkId,#logo,#NewStudentAddLinkId,#DepartmentAddLinkId,#AccountAddLinkId,#CurrentSemisterAddLinkId,#ResultAddLinkId,#CourseAddLinkId,#FeeAddLinkId,#CourseRegDateAddLinkId,#BacklogAddLinkId,#ResultLinkId,#RegBacklogInfo').click(function () {
                lodingFunction();
            });
            $("input[type='text']").css('height', '35px');
            $("input[type='password']").css('height', '35px');

            (function ($, window, document, undefined) {
                $(function () {
                    var $navigation = $('#navigation'), $navToggler = $('#navToggler');
                    $('#navToggler').on('click', function (e) {
                        e.preventDefault();
                        $navigation.toggleClass('expanded');
                    });
                });
            }(jQuery, this, this.document));



            var _gaq = _gaq || [];
            _gaq.push(['_setAccount', 'UA-36251023-1']);
            _gaq.push(['_setDomainName', 'jqueryscript.net']);
            _gaq.push(['_trackPageview']);

            (function () {
                var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
            })();