$("#IdCattletype").change(function () {

    $.ajax({
        type: "POST",
        url: '/Common/GetHerdsByCattleType/?idCattletype=' + $("#IdCattletype").val() ,
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

$("#Sex").change(function() {
    $.ajax({
        type: "POST",
        url: '/Common/GetType/?Sex=' + $("#Sex").val() + '&date=' + $("#DateBirth").val(),
        contentType: "application/json",
        success: function(data) {
            var optionStr = document.getElementById("IdCattletype");
            optionStr.length = 0;
            optionStr.options[0] = new Option('Select type', '');
            var notEmpty = true;
            $.each(data, function(key, value) {
                notEmpty = false;
                $('#IdCattletype')
                    .append($("<option></option>")
                        .attr("value", value.Value)
                        .text(value.Text));
            });
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



$(function () {

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


    if ($this.is(':checked')) {
        $('#newDisease').click();

    } else {
        $("#divDisease").removeClass("divDisease");
        $('#divDisease').empty();
    }

});


$('#IsGestation').click(function () {
    var $this = $(this);


    if ($this.is(':checked')) {
        $('#newGestation').click();

    } else {
        $("#divDeGestation").removeClass("divDeGestation");
        $('#divDeGestation').empty();
    }

});
$(document).ready(function () {
    $('.date').each(function () {
        //$(this).datepicker({ maxDate: "+1M +1D 1Y", regional: "fr" });
        //$(this).datepicker("option", "dateFormat", "dd.mm.yy");
        //$(this).datepicker($.datepicker.regional["fr"]);
    });
});
 function setSelect2Disease() {

    $("#CurrentDisease_IdDisease").select2();
    $("#IdMedicalTreatments").select2();
    
    //$("#CurrentDisease_StartDate").datepicker($.datepicker.regional["fr"]);

    //$("#StartDate").datepicker($.datepicker.regional["fr"]);


}

var setSelect2Gestation = function () {

    $("#CurrentGestation_DeathCalve").select2();
    //$("#CurrentGestation_StartDateGestation").datepicker({
    //    dateFormat: 'dd/mm/yy'
    //});

}

$(function () {
    //$("#DateBirth").datepicker($.datepicker.regional["fr"]);
    

    $("#IdCattletype").change(function () {
        var type = $("#IdCattletype option:selected").text();
        if (type === "Genisse plus 2 ans" || type === "Taries" || type === "Production") {

            $("#divGestation").show();

        } else {
            $("#divGestation").hide();
        }
    });


});


function NewGestation() {
    //$("#StartDateGestation").datepicker({dateFormat: 'dd/mm/yy'});
    $("#idAjaxFormG").addClass("AjaxForm");

    $("#newGestation").hide();
    $("#closeNewGestation").show();
}

$('#closeNewGestation').click(function () {
    $("#newGestation").show();
    $("#divGestation").empty();
    $("#closeNewGestation").hide();
    $("#idAjaxFormG").removeClass("AjaxForm");
});

function NewDisease() {
    $("#CurrentDisease_IdDisease").select2();
    $("#IdMedicalTreatments").select2();
    //$("#CurrentDisease_StartDate").datepicker({
    //    dateFormat: 'dd/mm/yy'
    //});
    //$("#CurrentDisease_EndDate").datepicker({
    //    dateFormat: 'dd/mm/yy'
    //});
    //$("#StartDate").datepicker({
    //    dateFormat: 'dd/mm/yy'
    //});
    $("#idAjaxFormD").addClass("AjaxForm");

    $("#newDisease").hide();
    $("#closeNewDisease").show();
}

$('#closeNewDisease').click(function () {
    $("#newDisease").show();
    $("#divDisease").empty();
    $("#closeNewDisease").hide();
    $("#idAjaxFormD").removeClass("AjaxForm");
});


$("#ListItem").change(function () {
    $("#viewPlaceHolder").empty();
    
    $url = "/" + $('#ListItem').val() + "/" + "Index";
    $("#viewPlaceHolder").load($url);

});


//Factorisez le code de création de dialogBox
//
//
//URGENT
function dialogBox(variable, title) {
    if (!title) title = "Dialog - ";
    //set the diaglog properties
    $('#' + variable + ' ').dialog({
        title: title,
        width: '600',
        modal: true,
        open: function (event, ui) {
            $('#' + variable + '  form').validate();
    }
    });
    $('#' + variable + ' ').dialog("open");
    $('#' + variable + '  select').select2();
    jQuery.validator.unobtrusive.parse('form');
}

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

function ValidateForm() {
    var isValid = 1;
    $('form').each(function () {
        $(this).find("[datarequired ='1']").each(function() {
            var elm = $(this);
            console.log(elm.attr('name'));
            if (!elm.val()) {
                isValid *= 0;
                var message = "Veuillez insérer un(e) " + $("[for = " + elm.attr('name')+ "]").text();
                $('[data-valmsg-for="' + elm.attr('name') + '"]').text(message);
            } else {
                isValid *= 1;
                $('[data-valmsg-for="' + elm.attr('name') + '"]').text("");
            }
        });
    });
    return isValid;
}

function submitForm(idForm) {
    if (ValidateForm() === 1) {
        $(idForm).submit();
    }
}