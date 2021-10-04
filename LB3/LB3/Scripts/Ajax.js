
$.ajaxSetup({
    xhrFields: {
        withCredentials: true
    }
});

let get_json_data_type = () => $('#json-type').val();
let get_xml_data_type = () => $('#xml-type').val();

var current_type;

let get_limit_val = () => $('#limit-input').val();
let get_sort_val = () => $('#set-sort').val();
let get_offset_val = () => $('#offset-input').val();

let get_minid_val = () => $('#minid-input').val();
let get_maxid_val = () => $('#maxid-input').val();
let get_like_val = () => $('#like-input').val();

let get_colums_val = () => $('#colums-input').val();
let get_global_like_val = () => $('#global-like-input').val();


var serverUrl = 'http://localhost:59089/api/students';

$("#btn-get-list").click(function () {

    if ($('.data-type:checked').val() == "json") {
        current_type = ".json";
    }
    else { current_type = ".xml"; }

    $.ajax({
        url: serverUrl + current_type,
        method: 'get',
        success: function (data) {
            $("#student-list").val(data.TEST_STUD.ID)
        },
    });
});

$("#btn-post").click(function () {
    $.ajax({
        url: serverUrl + "?RESULT=" + get_post_val(),
        method: 'post',
        dataType: 'JSON',
        data: jQuery.param({ RESULT: $("#txt-post").val() }),
        success: function (data) {
            $("#txt-stat").val("POST - success")
        },
    });
});

$("#btn-put").click(function () {
    $.ajax({
        url: serverUrl + "?ADD=" + get_put_val(),
        method: 'put',
        dataType: 'JSON',
        data: jQuery.param({ ADD: $("#txt-put").val() }),
        success: function (data) {
            $("#txt-stat").val("PUT - success")
        },
    });
});

$("#btn-delete").click(function () {
    $.ajax({
        url: serverUrl,
        method: 'delete',
        success: function (data) {
            $("#txt-stat").val("DELETE - success")
        },
    });
});