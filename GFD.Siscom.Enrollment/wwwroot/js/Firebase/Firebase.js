var firebaseConfig = {};

if (isAyuntamiento) {
    firebaseConfig = {
        apiKey: "AIzaSyCZKAlYIJ0jQNxaRzHxwn6FvpG-dwBwfFU",
        authDomain: "siscom-ayuntamiento.firebaseapp.com",
        databaseURL: "https://siscom-ayuntamiento.firebaseio.com",
        projectId: "siscom-ayuntamiento",
        storageBucket: "siscom-ayuntamiento.appspot.com",
        messagingSenderId: "732450663996",
        appId: "1:732450663996:web:8d07fa0108928261"
    }
} else {
    firebaseConfig = {
        apiKey: "AIzaSyB55vHO3bbzZH9gksaZpLMk9mHBg9-eR6c",
        authDomain: "siscom-sosapac.firebaseapp.com",
        databaseURL: "https://siscom-sosapac.firebaseio.com",
        projectId: "siscom-sosapac",
        storageBucket: "siscom-sosapac.appspot.com",
        messagingSenderId: "187900839364",
        appId: "1:187900839364:web:a33edbee502b16c8"
    }
}

var contDiscount = 0, contCancel = 0;
var dateCurrentlyAuth = new Date();
var defaultProject = firebase.initializeApp(firebaseConfig);
//console.log("defaultProject: ", defaultProject);
var databaseService = firebase.database();
//console.log("databaseService: ", databaseService);
var DiscountAuthorization = databaseService.ref("DiscountAuthorization");
var CancelRequest = databaseService.ref("CancelRequest");

DiscountAuthorization.on("child_added", function (snapshot, prevChildKey) {
    var list = snapshot.val();
    console.log("list Discount: ", list);
    var d = new Date(list.RequestDate);
    var validate = ValidateDate(dateCurrentlyAuth, d);
    if (list.Status == "Activo" && validate) {
        contDiscount = contDiscount + 1;
        console.log("list Discount: ", list);
    }
    $("#divNumberAuthDiscount").html(contDiscount > 0 ? `<span class="badge badge-pill badge-danger">${contDiscount}</span>` : "");
});

CancelRequest.on("child_added", function (snapshot, prevChildKey) {
    var list = snapshot.val();
    console.log("List CancelRequest: ", list);
    var d = new Date(list.DateRequest);
    var validate = ValidateDate(dateCurrentlyAuth, d);
    if (list.Status == "ESC01" && validate) {
        contCancel = contCancel + 1;
        console.log("List CancelRequest: ", list);
    }
    $("#divNumberAuthCancel").html(contCancel > 0 ? `<span class="badge badge-pill badge-danger">${contCancel}</span>` : "");
});


function ValidateDate(dateToday, dateFromList) {
    var d1 = new Date(dateToday);
    var d2 = new Date(dateFromList);
    if ((d1.getFullYear() == d2.getFullYear()) && (d1.getMonth() == d2.getMonth())) {
        var x = d1.getDate() - 1;
        if (d2.getDate() > x && d2.getDate() <= d1.getDate()) {
            return true;
        } 
    }
    return false;
}

