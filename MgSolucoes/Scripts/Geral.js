$("#Cpf").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
$("#Dt_Cadastro").inputmask("mask", { "mask": "99/99/9999" },{ placeholder: "mm/dd/yyyy" });
//$("#Dt_nascimento").inputmask("mask", { "mask": "99/99/9999" },{ placeholder: "dd/mm/yyyy" });

$("#Dt_Vencimento").inputmask("mask", { "mask": "99/99/9999" }, { placeholder: "mm/dd/yyyy" });
$("#Inicio").inputmask("mask", { "mask": "99/99/9999" }, { placeholder: "mm/dd/yyyy" });
$("#Fim").inputmask("mask", { "mask": "99/99/9999" }, { placeholder: "mm/dd/yyyy" });


$("#Dt_Nascimento").inputmask("mask", { "mask": "99/99/9999" },{ placeholder: "mm/dd/yyyy" });
$("#Dt_Cadastro").inputmask("mask", { "mask": "99/99/9999" }, { placeholder: "mm/dd/yyyy" });
//$("#Dt_cadastro").inputmask("mask", { "mask": "99/99/9999" }, { placeholder: "mm/dd/yyyy" });
$("#Dt_Pagamento").inputmask("mask", { "mask": "99/99/9999" }, { placeholder: "mm/dd/yyyy" });
$('#Valor_Credito').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#Valor_ofertado').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#Valor_Pago').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#element').priceFormat();

$('#Vl_credito_recebe').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 1,
    clearOnEmpty: true
});

$('#money').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 2,
    clearOnEmpty: true
});

$.each($('.money'), function () {
    var result = $(this).val();
    console.log(result);

});

$('.money').priceFormat({
    prefix: 'R$',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 2,
    clearOnEmpty: true
});


$('#Rep_1').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#Rep_2').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#Rep_3').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#Rep_4').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#Rep_5').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#Rep_6').priceFormat({
    prefix: '',
    centsSeparator: ',',
    thousandsSeparator: '.',
    centsLimit: 2,
    clearOnEmpty: true
});

$('#Tel_Fixo').focusout(function () {
    var phone, element;
    element = $(this);
    element.unmask();
    phone = element.val().replace(/\D/g, '');
    if (phone.length > 10) {
        element.mask("(99) 99999-999?9");
    } else {
        element.mask("(99) 9999-9999?9");
    }
}).trigger('focusout');

$('#Tel_Movel').focusout(function () {
    var phone, element;
    element = $(this);
    element.unmask();
    phone = element.val().replace(/\D/g, '');
    if (phone.length > 10) {
        element.mask("(99) 99999-999?9");
    } else {
        element.mask("(99) 9999-9999?9");
    }
}).trigger('focusout');


