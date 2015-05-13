$("#IdCattletype").change(function () {
    $.ajax({
        type: "POST",
        url: '/Common/GetHerdsByCattleType/?idCattletype=' + $("#IdCattletype").val(),
        contentType: "application/json",
        success: function (data) {
            var optionStr = document.getElementById("IdHerd");
            optionStr.length = 0;
            optionStr.options[0] = new Option('Select troupeau', '');
            var notEmpty = true;
            $.each(data, function (key, value) {
                notEmpty = false;
                $('#IdHerd')
                    .append($("<option></option>")
                    .attr("value", value.Value)
                    .text(value.Text));
            });
            if (notEmpty && $("#IdCattletype").val() != "") {
                $(function () {
                    var idCatlleType = $("#IdCattletype").val();
                    $("#IdCattleTypeDialog").text($("#IdCattletype").find("option:selected").text());


                    var opt = {
                        autoOpen: false,
                        bgiframe: true,
                        modal: true,
                        width: 550,
                        height: 650,
                        title: 'Details'
                    };

                    $(document).ready(function () {
                        $("#dialogContent").dialog(opt).dialog("open");
                    });
                });
            }
        }
    });
});

$("#btnNewHerd").click(function () {
    if ($("#IdCattletype").val() != "") {
        console.log("ok");
        var idCatlleType = $("#IdCattletype").val();
        $("#IdCattleTypeDialog").text($("#IdCattletype").find("option:selected").text());
        var opt = {
            autoOpen: false,
            modal: true,
            width: 550,
            height: 650,
            title: 'Details'
        };

        $(document).ready(function () {
            $("#dialogContent").dialog(opt).dialog("open");
        }); 
    }
});



$('#btnCreateHerd').click(function() {
    $.ajax({
        type: "POST",
        url: '/Common/CreateHerd/?idCattletype=' + $("#IdCattletype").val() + '&maxAnimals=' + $("#MaxAnimals").val() + '&label=' + $("#Label").val(),
        contentType: "application/json",
        success: function (data) {
            var optionStr = document.getElementById("IdHerd");
            optionStr.length = 0;
            optionStr.options[0] = new Option('Select troupeau', '');
            var empty = true;
            $.each(data, function (key, value) {
                empty = false;
                $('#IdHerd')
                    .append($("<option></option>")
                    .attr("value", value.Value)
                    .text(value.Text));
            });
            if (!empty) {
                $("#dialogContent").css("display", "none");
            } else {
                $("#idMsgError").text("Erreur produit en ajoutant un nouveau troupeau! - réessayer");
  
            }

        }
    });
});


$(document).ready(function () {

    (function ($) {

        $('#filter').keyup(function () {

            var rex = new RegExp($(this).val(), 'i');
            $('tr.searchable').hide();
            $('tr.searchable').filter(function () {
                return rex.test($(this).text());
            }).show();

        })

    }(jQuery));

});


//jquery action for disease
$('#HealthState').click(function() {
    var $this = $(this);
    var isSend = true;
     
    if ($this.is(':checked')) {
        

        $.ajax({
            type: "POST",
            url: '/Common/GetDisease' ,
            contentType: "application/json",
            success: function(data) {
                var optionStr = document.getElementById("medicalTreatment");
                optionStr.length = 0;
                optionStr.options[0] = new Option('Select maladie', '');
                var notEmpty = true;
                $.each(data, function(key, value) {
                    notEmpty = false;
                    $('#medicalTreatment')
                        .append($("<option></option>")
                            .attr("value", value.Value)
                            .text(value.Text));
                });
            }
        });

        $.ajax({
            type: "POST",
            url: '/Common/GetMedicalTreatment',
            contentType: "application/json",
            success: function (data) {
                var optionStr = document.getElementById("disease");
                optionStr.length = 0;
                optionStr.options[0] = new Option('Select traitement', '');
                var notEmpty = true;
                $.each(data, function (key, value) {
                    notEmpty = false;
                    $('#disease')
                        .append($("<option></option>")
                            .attr("value", value.Value)
                            .text(value.Text));
                });
            }
        });




        var opt = {
            autoOpen: false,
            bgiframe: true,
            modal: true,
            width: 550,
            height: 650,
            title: 'Details'
        };

        $(document).ready(function() {
            $("#dialogHealth").dialog(opt).dialog("open");
        });
    }

    if (isSend) {
        
        $('#HealthState').attr('checked', false);
    }
});

