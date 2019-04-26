$(document).ready(function () {
    if (document.getElementById('apiAddress') !== null) {
        if (localStorage.getItem("apiAddress") === null || (localStorage.getItem("apiAddress") !== document.getElementById('apiAddress').innerHTML)) {
            localStorage.setItem('apiAddress', document.getElementById('apiAddress').innerHTML);
        }
    }
    console.log('documentReady');
});


if (localStorage.getItem("apiAddress") !== null) {
    timerJob();
    timerJob2();
}

$(".uppercase").keyup(function () {
    var text = $(this).val();
    $(this).val(text.toUpperCase());
}); 

function clearErrors() {
    $(".validation-summary-errors").empty();
};

function convertPrice(s) {
    return Math.round(s) + ",00";
}

let numberOfProducts = 10;
const oneSecond = 1000; //!!!
let windowStep = 0;



function timerJob() {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState === XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
            if (xmlhttp.status === 200) {
                let products = JSON.parse(xmlhttp.responseText);
                updateOnlineOverdrive(products);
            }
            else if (xmlhttp.status === 400) {
                alert('There was an error 400');
            }
            else {
                alert('something else other than 200 was returned: ' + xmlhttp.status);
            }
        }
    };
    if (localStorage.getItem("apiAddress") !== null) {
        xmlhttp.open("GET", localStorage.getItem("apiAddress") + "/api/Product/", true);
        xmlhttp.withCredentials = false;
        xmlhttp.send();
    }
}

function timerJob2() {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState === XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
            if (xmlhttp.status === 200) {
                let products = JSON.parse(xmlhttp.responseText);
                updatePriceOverdrive(products);
            }
            else if (xmlhttp.status === 400) {
                alert('There was an error 400');
            }
            else {
                alert('something else other than 200 was returned: ' + xmlhttp.status);
            }
        }
    };

    if (localStorage.getItem("apiAddress") !== null) {
        xmlhttp.open("GET", localStorage.getItem("apiAddress") + "/api/Product/", true);
        xmlhttp.withCredentials = false;
        xmlhttp.send();
    }
}

function updateOnline(product) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("PUT", localStorage.getItem("apiAddress") + '/api/write/Product/online/' + product.productId, true);
    xmlhttp.setRequestHeader('Content-type', 'application/json; charset=utf-16');
    xmlhttp.withCredentials = false;
    xmlhttp.send(JSON.stringify(product));
}

function updateLocked(product) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("PUT", localStorage.getItem("apiAddress") + '/api/write/Product/locked/' + product.productId, true);
    xmlhttp.setRequestHeader('Content-type', 'application/json; charset=utf-16');
    xmlhttp.withCredentials = false;
    xmlhttp.send(JSON.stringify(product));
}

function updatePrice(product) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("PUT", localStorage.getItem("apiAddress") + '/api/Product/' + product.productId, true);
    xmlhttp.setRequestHeader('Content-type', 'application/json; charset=utf-16');
  //xmlhttp.withCredentials = false;
    product.userIdentifier = "Lenovo";
    var test = JSON.stringify(product);
    xmlhttp.send(JSON.stringify(product));
}

function updateOnlineOverdrive(products) {
    if (products.length === 0) {
        setTimeout(timerJob, oneSecond);
        console.log("No products found!");
        return;
    }
  //  numberOfProducts = products.length;
    numberOfProducts = 10;
    const selectedItem = Math.floor(Math.random() * numberOfProducts);
    let selectedProduct = products[selectedItem];
    if (selectedProduct.locked === true) {
        console.log(selectedProduct.productId + " is Locked for uppdating of Online/Offline!");
        setTimeout(timerJob, oneSecond);
        return;
    }
    selectedProduct.online = !selectedProduct.online;
    updateOnline(selectedProduct);
    const onlineSelector = `#${selectedProduct.productId + "_2"} td:eq(5)`;
    const pendingSelector = `#${selectedProduct.productId + "_2"} td:eq(6)`;
    if (selectedProduct.online === true) {
        $(onlineSelector).text("Online");
        $(onlineSelector).removeClass("alert-danger");      
        $(pendingSelector).text("");     
        console.log(selectedProduct.productId + " is Online!");
    }
    else {
        $(onlineSelector).text("Offline");
        $(onlineSelector).addClass("alert-danger");
        $(pendingSelector).text("Update");      
        console.log(selectedProduct.productId + " is Offline!");
    }
    if (document.getElementById("All") !== null) {
        //doFiltering();
    }
    let interval = Math.round(oneSecond / numberOfProducts);
    setTimeout(timerJob, interval);
}

function updatePriceOverdrive(products) {
    if (products.length === 0) {
        setTimeout(timerJob2, oneSecond);
        console.log("No products found!");
        return;
    }

   // numberOfProducts = products.length;
    numberOfProducts = 10; //vain kymmen ekaa...
    const selectedItem = Math.floor(Math.random() * numberOfProducts);
    let selectedProduct = products[selectedItem];
    updateOnline(selectedProduct);
    if (selectedProduct.locked === true) {
        console.log(selectedProduct.productId + " is Locked for uppdating of Online/Offline!");
        let interval = Math.round(oneSecond / numberOfProducts);
        setTimeout(timerJob2, interval);
        return;
    }

    const delta = selectedProduct.listPrice / 10;
  //  selectedProduct.listPrice = Math.round(selectedProduct.listPrice + delta); //Increase price 10%
    selectedProduct.listPrice = Math.round(selectedProduct.listPrice + 1); 
    
    updatePrice(selectedProduct);

    const priceSelector = `#${selectedProduct.productId + "_2"} td:eq(2)`;
    if (selectedProduct.online === true) {
        $(priceSelector).text(convertPrice(selectedProduct.listPrice));
    }
    else {
        $(priceSelector).text(convertPrice(selectedProduct.listPrice));
    }
    setTimeout(timerJob2, oneSecond);
}

function doFiltering() {

    let selection = 0;
    let radiobtn = document.getElementById("All");
    if (radiobtn.checked === false) {
        radiobtn = document.getElementById("Online");
        if (radiobtn.checked === true) {
            selection = 1;
        }
        else {
            selection = 2;
        }
    }

    var table = $('#products > tbody');
    $('tr', table).each(function () {
        $(this).removeClass("hidden");
        let td = $('td:eq(5)', $(this)).html();
        if (td !== undefined) {
            td = td.trim();
        }
        if (td === "Offline" && selection === 1) {
            $(this).addClass("hidden");  //Show only Online
        }
        if (td === "Online" && selection === 2) {
            $(this).addClass("hidden"); //Show only Offline
        }
    });
}