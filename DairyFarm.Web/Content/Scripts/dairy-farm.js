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

$("#Sex").change(function () {
    var sex = $("#Sex").val();
    var db = $("#DateBirth").val();
    if (sex != null && db != null) {
        getType(sex, db);
    }
});
$("#DateBirth").change(function () {
    var sex = $("#Sex").val();
    var db = $("#DateBirth").val();
    if (sex != null && db != null) {
        getType(sex,db);
    }
});
function getType(sex,date) {
    
        $.ajax({
    type: "POST",
    url: '/Common/GetType/?Sex=' + sex + '&date=' + date,
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
};

$("#btnNewHerd").click(function () {
    if ($("#IdCattletype").val() != "") {
        console.log("ok");
        var idCatlleType = $("#IdCattletype").val();
        $("#IdCattleTypeDialog").text($("#IdCattletype").find("option:selected").text());
        var opt = {
            autoOpen: false,
            modal: true,
            width: 600,
            title: 'Details'
        };

        $(document).ready(function () {
            $("#dialogContent").dialog(opt).dialog("open");
        }); 
    }
});


$('#submitHerd').click(function () {
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
    $('.select2').attr('style', '');
    
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

function ValidateForm(idForm) {
    var lengthM = $("#MaxAnimals").val();
    console.log(lengthM);
    var isValid = 1;
        $(idForm).find("[datarequired ='1']").each(function () {
            var elm = $(this);
            if (!elm.val()) {
                isValid *= 0;
                console.log(elm.attr('name'));
                var message = "Veuillez insérer un(e) " + $('[for = ' + elm.attr('name')+ ']').text();
                $('[data-valmsg-for="' + elm.attr('name') + '"]').text(message);
            } else {
                isValid *= 1;
                $('[data-valmsg-for="' + elm.attr('name') + '"]').text("");
            }
    });
    return isValid;
}

function submitForm(idForm) {
    console.log(idForm);
    if (ValidateForm(idForm) === 1) {
        $(idForm).submit();
    }
}

function ValidCode() {
    var lengthM = $("#MalParent").val().length;
    var lengthF = $("#FemaleParent").val().length;
    if (lengthM != 9) {
        var message = "Veuillez insérer un code de 9 chiffres";
        $('[data-valmsg-for="MalParent"]').text(message);
    } else {
        $('[data-valmsg-for="MalParent"]').text("");

        $("#SubmitEditCattle").removeAttr("disabled");
    }
    if (lengthF != 9) {
        var message = "Veuillez insérer un code de 9 chiffres";
        $('[data-valmsg-for="FemaleParent"]').text(message);
        $("#SubmitEditCattle").attr("disabled", "disabled");
    } else {
        $('[data-valmsg-for="FemaleParent"]').text("");
        $("#SubmitEditCattle").removeAttr("disabled");
    }
    CheckLabelType();
}

function CheckLabelType() {
 
    var code = $("#Label").val();
    $.post("/Common/ChechUniqueCattleType/" + code,
        function (data, state) {
            if (data === "False") {
                $('[data-valmsg-for="Label"]').text("Existe déja");
                $("#submitHerd").attr("disabled", "disabled");
                
            } else {
                $("#submitHerd").removeAttr("disabled");
                $('[data-valmsg-for="Label"]').text("");
            }
        });
}


function checkQuantity() {
    var code = $("#Quantity").val();
    var label = $("#IdFood option:selected").text();
    var qty;
    var ok;
    $.ajax({
        type: "POST",
        url: '/Common/ChechQtyFood/' + code + "/" + label,
        contentType: "application/json",
        success: function (data) {
            $.each(data, function (key, value) {
                qty = value.Qty;
                ok = value.ok;
            });
            console.log(qty, ok);
            if (ok === 0) {
                $('[data-valmsg-for="Quantity"]').text("La quantité maximum est de " + qty +" kg");
                $("#submitHerd").attr("disabled", "disabled");
            } else {
                $("#submitHerd").removeAttr("disabled");
                $('[data-valmsg-for="Quantity"]').text("");
            }

        }
    });
}

function checkHerdMax() {
    
    var lengthM = $("#MaxAnimals").val();
    console.log(lengthM);
    if (lengthM <0 || lengthM>50) {
        var message = "Le nombre doit être compris entre 1 et 50";
        $("#submitHerd").attr("disabled", "disabled");
        $('[data-valmsg-for="MaxAnimals"]').text(message);
    } else {
        $('[data-valmsg-for="MaxAnimals"]').text("");

        $("#submitHerd").removeAttr("disabled");
    }
}

function checkUniqueHerd() {
    var code = $("#Label").val();
    $.post("/Common/ChechUniqueHerd/" + code,
        function(data, state) {
            if (data === "False") {
                $('[data-valmsg-for="Label"]').text("Existe déja");
                $("#submitHerd").attr("disabled", "disabled");

            } else {
                $("#submitHerd").removeAttr("disabled");
                $('[data-valmsg-for="Label"]').text("");
            }
        });
}