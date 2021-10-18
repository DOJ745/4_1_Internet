/*$.ajaxSetup({
    xhrFields: {
        withCredentials: true
    }
});*/

const SERVER = "http://localhost:59089/";
const CONTROLLER_DEFAULT_PATH = SERVER + 'api/students/';

let pagesNumber = 0;
let StudentList = [];

let curOffset = 0;
let curLimit = 5;

let updateForm = $(`#update-form`);
let addForm = $(`#add`);
let searchForm = $(`#search`);

let isXml = () => $("#xml").prop("checked");

function addStudentHandler() {

    addForm.find("button").on('click', () => {
        let requestParams = {
            url: CONTROLLER_DEFAULT_PATH,
            type: 'POST',
            data: addForm.serialize()
        };

        $.ajax(requestParams)
            .done((res) => {
                if (isXml()) res = parser.parse(res).Root;
                let studRes = 'Student ' + res.Name + ' successfuly added! ID - #' + res.Id;
                showAlert("success", studRes);
            })

            .fail((jqXhr, textStatus, errorThrown) => {
                showAlert('danger', errorThrown);
            });
    });
}

function removeStudent(id) {

    let requestParams = {
        url: (CONTROLLER_DEFAULT_PATH + id),
        type: 'delete'
    };

    $.ajax(requestParams)
        .done((res) => {
            if (isXml()) res = parser.parse(res).Root;
            showAlert('info', 'Student ' + res.Name + ' deleted.');
            getAllStudents();
        })

        .fail((jqXhr, textStatus, errorThrown) => {
            showAlert('danger', errorThrown);
        });

}

function searchStudentsHandler() {

    searchForm.find("button").on('click', () => {
        searchStudents(false);
    });

}

function searchStudents(all) {

    let requestParams = {
        url: `${CONTROLLER_DEFAULT_PATH}?${searchForm.serialize()}`,
        type: 'get',
        headers: {
            'Accept': isXml() ? 'application/xml' : 'application/json'
        }
    };

    $.ajax(requestParams)
        .done((res) => {
            if (isXml()) res = parser.parse(
                new XMLSerializer().serializeToString(res.documentElement)).ArrayOfStudent.Student;

            StudentList = res;
            updateStudentList(StudentList);
        })

        .fail((jqXhr, textStatus, errorThrown) => {
            showAlert('danger', errorThrown);
        });
}

function showAlert(type, text) {

    if (type == '' || type == undefined)
        type = 'info';

    $('#alert-text').text(text);
    
    $('#alert').toggleClass('alert-' + type);

    $('#alert').fadeIn().delay(3000).fadeOut();
}

function updateStudentList(students) {

    $('#student-list > tbody').empty();
    for (let index in students) {

        let s = students[index];

        let rBtn = '<button onclick="removeStudent(' + s.Id + ')" class="btn btn-danger">Remove #' + s.Id + '</button>';
        let eBtn = '<button onclick="showEditModal(' + index + ')" class="btn btn-info">Edit</button>';

        let rLink = '<a href="'
            + s._links.self + '">student/'
            + s.Id + '</a>';

        let sRow = '<tr id="student-'
            + s.Id + '"><td>'
            + (s.Id == -1 ? "" : s.Id) + '</td><td>'
            + s.Name + '</td><td>'
            + s.Phone + '</td><td>'
            + rBtn + ' ' + eBtn + '</td><td>'
            + rLink + '</td><tr>';

        $('#student-list > tbody').append(sRow);
    }
}

function updateStudent(sId) {

    let requestParams = {
        url: CONTROLLER_DEFAULT_PATH + sId,
        type: 'put',
        data: updateForm.serializeArray()
    };

    $.ajax(requestParams)
        .done((res) => {
            if (isXml()) res = parser.parse(res).Root;
            let sRes = 'Student successfuly updated!' + res.Id;
            showAlert("success", sRes);
        })

        .fail((jqXhr, textStatus, errorThrown) => {
            showAlert('danger', errorThrown);
        });
}

function showEditModal(studentIndex) {

    let student = StudentList[studentIndex];

    $('#update-id').text(student.Id);
    $('#update-form input[name="id"]').val(student.Id);
    $('#update-form input[name="name"]').val(student.Name);
    $('#update-form input[name="phone"]').val(student.Phone);
    $('#update-modal').modal('toggle');

    updateForm.find("button").on('click', () => {
        updateStudent(student.Id);
        $('#update-modal').modal('toggle');
    });

}


function offsetAndLimitOnChange() {

    curOffset = searchForm.find('input[name="offset"]').val();
    curLimit = searchForm.find('input[name="limit"]').val();

}

function initForms() {

    addStudentHandler();
    searchStudentsHandler();
    searchForm.find('input[name="offset"], input[name="limit"]')
        .on('change', offsetAndLimitOnChange);

    searchStudents(true);
}


(function () { initForms(); }) ();