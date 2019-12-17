function CategoryList() {
    $("#tblCategory").html(""); 
    var value = $.cookie("authenticateUser");
    $.ajaxSetup({
        headers: {
            'Authorization': 'Bearer ' + value
        }
    });
    $.getJSON("http://localhost:4040/api/Categories", function (data) {
        $.each(data, function (i, item) {
            $("#tblCategory").append( 
                "<tr><td>" + item.id + "</td>" +
                "<td>" + item.name + "</td>" +
                "<td>" + item.parentId + "</td>" +
                "<td>" + item.hierarchy + "</td>" +
                "<td><input type='submit' class='btn btn-primary' onclick='CategoryFetch(\"" + item.id + "\");' value='Update' /></td>" +
                "<td><input type='submit' class='btn btn-danger' onclick='CategoryDelete(\"" + item.id + "\");' value='Delete' /></td></tr>" 
            );
        });
    })

}

function CategoryAdd() {
    var value = $.cookie("authenticateUser");
    var val = $("#CategoryAddForm").serializeFormJSON();
    var parentId = $('#ParentId option:selected').attr("id");
    val.ParentId = parentId;
    $.ajaxSetup({
        headers: {
            'Authorization': 'Bearer ' + value,
            'Content-Type': 'application/json'

        }
    });
    $.ajax({
        type: "POST",
        url: "http://localhost:4040/api/Categories",
        data: JSON.stringify(val),
        success: function () {
            alert("Category Added");
        }
    });
}

function CategoryFetch(id) {
    var value = $.cookie("authenticateUser");
    $.ajaxSetup({
        headers: {
            'Authorization': 'Bearer ' + value
        }
    });
    $.getJSON("https://localhost:44310/api/Categories/" + id)
        .done(function(data) {
            $("#CategoryUpdateForm #CategoryId").val(data.id);
            $("#CategoryUpdateForm #CategoryName").val(data.name);
            $("#CategoryUpdateForm #ParentId").val(data.parentId);
            $("#CategoryUpdateForm #Hierarchy").val(data.hierarchy);

            $("#CategoryUpdateForm #btnCategoryUpdate").attr("onclick", "CategoryUpdate();");
        });
}

function CategoryUpdate() {
    var value = $.cookie("authenticateUser");
    var val = $("#CategoryUpdateForm").serializeFormJSON();
    $.ajaxSetup({
        headers: {
            'Authorization': 'Bearer ' + value,
            'Content-Type': 'application/json'
}
    });
    $.ajax({
        type: "PUT",
        url: "http://localhost:4040/api/Categories",
        data: JSON.stringify(val),
        success: function() {
            alert("Category Updated");
        }
    });
}

function CategoryDelete(id) {
    var value = $.cookie("authenticateUser");
    $.ajaxSetup({
        headers: {
            'Authorization': 'Bearer ' + value
        }
    });
    $.ajax({
        type: "DELETE",
        url: "https://localhost:44310/api/Categories/" + id,
        success: function() {
            alert("Category Deleted");
        }
    });
}

(function ($) {
    $.fn.serializeFormJSON = function () {

        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
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