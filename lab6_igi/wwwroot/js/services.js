$('.client-part-highlighter').hover(function () {
    $('#client-part').addClass('selected-row');
});

$('.client-part-highlighter').mouseout(function () {
    $('#client-part').removeClass('selected-row');
});

$(window).load(function () {
    $.ajax({
        method: 'GET',
        contentType: "application/json",
        url: '../api/services/',
        success: function (responce) {
            for (let res in responce.serv) {
                res = responce.serv[res];
                let htmlBlock =
                    '<div class="row content-row" id="service-' + res.serviceID + '">' +
                    '<div class="col-md-2" id="name-' + res.serviceID + '">' + res.customerName + '</div>' +
                    '<div class="col-md-2" id="summ-' + res.serviceID + '">' + res.summ + '</div>' +
                        '<div class="col-md-2" id="discount-' + res.serviceID + '">' + res.discountType.name + '</div>' +
                        '<div class="col-md-2" id="type-' + res.serviceID + '">' + res.type.name + '</div>' +
                    '</div >';
                $('#table-content').append(htmlBlock);
            }
            for (let res in responce.disc) {
                res = responce.disc[res];
                $('#picked-discount').append(
                    '<option id="discount-' + res.discountID + '">'
                    + res.name
                    + '</option>');
            }
            for (let res in responce.type) {
                res = responce.type[res];
                $('#picked-type').append(
                    '<option id="type-' + res.typeOfServiceID + '">'
                    + res.name
                    + '</option>');
            }

            $('.content-row').click(function () {
                let pickedId = Number(('#' + $(this).prop('id')).slice(9));
                let pickedName = $('#name-' + pickedId).html();
                let pickedsumm = $('#summ-' + pickedId).html();
                let pickedDiscount = $('#discount-' + pickedId).html();
                let pickedType = $('#type-' + pickedId).html();

                $('#picked-id').val(pickedId);
                $('#picked-name').val(pickedName);
                $('#picked-summ').val(pickedsumm);
                $('#picked-discount').val(pickedDiscount);
                $('#picked-type').val(pickedType);

                $('.content-row').removeClass('selected-row');
                $(this).addClass('selected-row');

            });
        }
    })
    $.ajax({

    });
});

function getId(elem) {
    let result
    $('#picked-' + elem).children().toArray().forEach(function (element) {
        if (element.textContent == $('#picked-' + elem).val()) {
            result =  element.getAttribute('id').slice((elem).length + 1);
        }
    });
    return result;
}
$('#add').click(function () {
    $.ajax({
        data: JSON.stringify({
            CustomerName: $('#picked-name').val(),
            Summ: $('#picked-summ').val(),
            DiscountId: Number(getId('discount')),
            TypeOfServiceID: Number(getId('type'))
        }),
        method: 'POST',
        contentType: "application/json",
        url: '../api/Services',
        success: function () {
            reload();
        }
    });
});

$('#delete').click(function () {
    $.ajax({
        method: 'DELETE',
        url: '../api/Services/' + $('#picked-id').val(),
        success: function () {
            reload();
        },
        error: function () {
            alert('Element wasn\'t found');
        }
    });
});

$('#edit').click(function () {
    $.ajax({
        data: JSON.stringify({
            ServiceId: $('#picked-id').val(),
            CustomerName: $('#picked-name').val(),
            Summ: $('#picked-summ').val(),
            DiscountId: Number(getId('discount')),
            TypeOfServiceID: Number(getId('type'))
        }),
        method: 'PUT',
        contentType: "application/json",
        url: '../api/Services/' + $('#picked-id').val(),
        success: function () {
            reload();
        },
        error: function () {
            alert('Element wasn\'t found');
        }
    });
});



function reload() {
    location.reload();
}

