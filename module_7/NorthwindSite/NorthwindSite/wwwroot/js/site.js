// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function() {

    $('.btn-newProduct').on('click',
        function () {
            $('.products__popup #form').attr('action', '/Home/CreateProduct');
            $('.products__popup').addClass("products__popup-on");
            $('.products__popup form')[0].reset();
        });

    $('.products__overlay').on('click',
        function () {
            $('.products__popup').removeClass("products__popup-on");
        });

    $('.products__btn-close').on('click',
        function () {
            $('.products__popup').removeClass("products__popup-on");
        });

    $('.btn-updateProduct').on('click',
        function () {

            $('.products__popup #form').attr('action', '/Home/UpdateProduct');

            $('.products__popup').addClass("products__popup-on");

            var productId = $(this).parent().siblings('[data-id]').first().attr('data-id');
            $('.products__popup #productId').val(productId);

            var productName = $(this).parent().siblings('[productName]').first().text();
            $('.products__popup #productName').val(productName);

            var companyName = $(this).parent().siblings('[companyName]').first().text();
            $('.products__popup #companyName').val(companyName);

            var categoryName = $(this).parent().siblings('[categoryName]').first().text();
            $('.products__popup #categoryName').val(categoryName);

            var qtyPerUnit = $(this).parent().siblings('[qtyPerUnit]').first().text();
            $('.products__popup #qtyPerUnit').val(qtyPerUnit);

            var unitPrice = $(this).parent().siblings('[unitPrice]').first().text();
            $('.products__popup #unitPrice').val(unitPrice);

        });

    $('.myBtn').on('click', function () {
         $('.myNavbar').toggleClass("myNavbar__on");
     });

    
});
