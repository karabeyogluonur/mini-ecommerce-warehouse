function deleteProduct(id) {
    Swal.fire({
        title: 'Bu ürünü silmek istediğinize emin misiniz?',
        showDenyButton: true,
        confirmButtonText: 'Evet',
        denyButtonText: `Vazgeç`,
    })
        .then((result) => {
            if (result.isConfirmed) {
                axios.get(`/product/delete/${id}`)
                    .then(function (res) {
                        if (res.status == 200) {
                            Swal.fire(res.data, '', 'success');
                            $(`tr[itemid = ${id}]`).remove();
                        } else {
                            Swal.fire(res.data, '', 'error');
                        }
                    })
                    .catch(function (err) {
                        Swal.fire(err.response.data, '', 'error');
                    });
            } else if (result.isDenied) {
                Swal.fire('Talebiniz iptal edildi!', '', 'info');
            }
        })
}


function openAddStockModal(id) {
    $("input[name=ProductId]").val(id);
}

function addStock() {
    var productId = $("input[name=ProductId]").val();
    var quantity = $("input[name=Quantity]").val();
    var comment = $("textarea[name=Comment]").val();

    if (quantity == null || quantity == 0) {
        toastr["error"]("Lütfen tüm alanları eksiksiz giriniz");
    }
    else {
        axios.post("/product/addStock", {
            productId: productId,
            quantity: quantity,
            comment: comment
        }).then(response => {
            if (response.status == 200) {
                toastr["success"]("Stok ekleme başarılı!");
                updateListStock(productId, quantity);
            }
            else {
                toastr["error"](response.data);
            }
        }).catch(error => {
            toastr["error"](error.response.data);
        });
    }
    
}

function updateListStock(productId, quantity) {

    var stockText = $(`tr[itemid=${productId}] .product-stock`).text();
    var newStock = parseInt(stockText) + parseInt(quantity);
    $(`tr[itemid=${productId}] .product-stock`).html(newStock);

}