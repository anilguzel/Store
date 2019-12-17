function ProductList() {
    $("#tblProduct").html("");
    var value = $.cookie("authenticateUser");
    var claimType = $.cookie("claimType");
    if (claimType === "product_view") {
        $.ajaxSetup({
            headers: {
                'Authorization': 'Bearer ' + value
            }
        });
        $.getJSON("http://localhost:4040/api/Products", function (data) {
            $.each(data, function (i, item) {
                $("#tblProduct").append(
                    "<tr><td>" + item.id + "</td>" +
                    "<td>" + item.name + "</td>" +
                    "<td>" + item.description + "</td>" +
                    "<td>" + item.imageUrl + "</td>" +
                    "<td>" + item.price + "</td>" +
                    "<td>" + item.categoryId + "</td>" +
                    //"<td><a class='btn btn-primary' href='../" + item.name + "' >Detail</a></td>" +
                    "<td><input type='submit' class='btn btn-primary' onclick='ProductFetch(\"" + item.id + "\");' value='Update' /></td>" +
                    "<td><input type='submit' class='btn btn-danger' onclick='ProductDelete(\"" + item.id + "\");' value='Delete' /></td></tr>"
                );
            });
        });
    }


}

function ProductAdd() {
    var value = $.cookie("authenticateUser");
    var val = $("#ProductAddForm").serializeFormJSON();
    
    var catId = $('#CategoryId2 option:selected').attr("id");
    val.CategoryId = catId;
    delete val.CategoryId1;
    delete val.CategoryId2;
    $.ajaxSetup({
        headers: {
            'Authorization': 'Bearer ' + value,
            'Content-Type': 'application/json'
        }
    });
    $.ajax({
        type: "POST",
        url: "http://localhost:4040/api/Products",
        data: JSON.stringify(val),
        success: function () {
            alert("Product Added");
        }
    });
}

function ProductFetch(id) {
    var value = $.cookie("authenticateUser");
    $.ajaxSetup({
        headers: {
            'Authorization': 'Bearer ' + value
        }
    });
    $.getJSON("https://localhost:44310/api/Products/" + id)
        .done(function(data) {
            $("#ProductUpdateForm #ProductId").val(data.id);
            $("#ProductUpdateForm #ProductName").val(data.name);
            $("#ProductUpdateForm #Description").val(data.description);
            $("#ProductUpdateForm #Price").val(data.price);
            $("#ProductUpdateForm #btnProductUpdate").attr("onclick", "ProductUpdate();");

            //$.ajax({
            //    url: "http://localhost:4040/api/categories/",
            //    type: "get",
            //    success: function(data) {

            //        $.each(data,
            //            function(i) {
            //                $('#CategoryId')
            //                    .append("<option id=" + data[i].id + ">" + data[i].name + "</option>");
            //            });
            //    }
            //});
        });
}

function ProductUpdate() {
        var value = $.cookie("authenticateUser");
        var val = $("#ProductUpdateForm").serializeFormJSON();
        $.ajaxSetup({
            headers: {
                'Authorization': 'Bearer ' + value,
                'Content-Type': 'application/json'

            }
        });
        $.ajax({
            type: "PUT",
            url: "http://localhost:4040/api/Products",
            data: JSON.stringify(val),
            success: function () {
                alert("Product Updated");
            }
        });
    }

    function ProductDelete(id) {
        var value = $.cookie("authenticateUser");
        $.ajaxSetup({
            headers: {
                'Authorization': 'Bearer ' + value
            }
        });
        $.ajax({
            type: "DELETE",
            url: "https://localhost:44310/api/Products/" + id,
            success: function () {
                alert("Product Deleted");
            }
        });
    }

    $(document).ready(function () {
        var value = $.cookie("authenticateUser");
        $.ajaxSetup({
            headers: {
                'Authorization': 'Bearer ' + value
            }
        });
        $.ajax({
            url: "http://localhost:4040/api/categories/h/0",
            type: "get",
            success: function (data) {
                $.each(data,
                    function (i) {
                        $('#CategoryId')
                            .append("<option id=" + data[i].id + ">" + data[i].name + "</option>");
                    });
            },
            //error: function(msg) { alert(msg); }
        });
    });
    (function ($) {
        $.fn.serializeFormJSON = function () {

            var o = {};
            var a = this.serializeArray();
            $.each(a,
                function () {
                    if (o[this.name]) {
                        if (!o[this.name].push) {
                            o[this.name] = [o[this.name]];
                        }
                        o[this.name].push(this.value || '');
                    } else {
                        o[this.name] = this.value || '';
                    }
                });
            return o;
        };
    })(jQuery);