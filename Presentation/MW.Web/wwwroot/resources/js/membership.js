function deleteUser(id) {
    Swal.fire({
        title: 'Bu kullanıcıyı silmek istediğinize emin misiniz?',
        showDenyButton: true,
        confirmButtonText: 'Evet',
        denyButtonText: `Vazgeç`,
    })
        .then((result) => {
            if (result.isConfirmed) {
                axios.get(`/user/delete/${id}`)
                    .then(function (res) {
                        if (res.status == 200) {
                            Swal.fire(res.data, '', 'success');
                            $(`tr[itemid = ${id}]`).remove();
                        } else {
                            Swal.fire(res.data, '', 'error');
                        }
                    })
                    .catch(function (err) {
                        Swal.fire('Talebiniz bilinmeyen bir hata nedeniyle gerçekleştirilemedi!', '', 'error');
                    });
            } else if (result.isDenied) {
                Swal.fire('Talebiniz iptal edildi!', '', 'info');
            }
        })
}

function authentication() {

    var email = $("#email").val();
    var pass = $("#password").val();

    if (email == "" || pass == "") {

        Swal.fire("Lütfen email ve şifre alanlarını giriniz!", "", "info");
    }
    else {
        axios.post("/auth/login", {
            email: email,
            password: pass
        }).then(response => {
            if (response.status == 200) {
                Swal.fire("Giriş başarılı. Yönlendiriliyorsunuz!", "", "success");
                setTimeout(function () { window.location = "/home/index"; }, 2000);
            }
            else {
                Swal.fire(response.data, "", "info");
            }
        }).catch(error => {
            Swal.fire(error.response.data, "", "error");
        });
    }
}