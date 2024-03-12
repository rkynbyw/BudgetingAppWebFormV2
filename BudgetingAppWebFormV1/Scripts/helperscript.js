$(document).ready(function () {
    function formatRupiah(amount) {
        var number_string = amount.toString();
        var sisa = number_string.length % 3;
        var rupiah = number_string.substr(0, sisa);
        var ribuan = number_string.substr(sisa).match(/\d{3}/g);

        if (ribuan) {
            separator = sisa ? '.' : '';
            rupiah += separator + ribuan.join('.');
        }

        return 'Rp ' + rupiah;
    }

    function formatAmount() {
        var amount = $("#txtAmount").val();
        var formattedAmount = formatRupiah(amount);
        $("#txtAmount").val(formattedAmount);
    }

    formatAmount();

    $("#txtAmount").on('input', function () {
        formatAmount();
    });
});
