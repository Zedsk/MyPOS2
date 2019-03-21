
/* ========================================================================
 * Scripts javascript for 
 * MyPOS
 * ======================================================================== */

/*
 * Scripts for
 * Transaction views
* ======================================================================== */

function ButtonCalc_Click(id) {
    var val = id.getAttribute('Value');
    document.getElementById('addProduct').value += val;
}

function ButtonDelete_Click() {
    var val = document.getElementById('addProduct').value;
    document.getElementById('addProduct').value = val.substring(0, val.length - 1);
}

function ButtonAddProduct_Click() {
    var minus = false;
    CreateRquestAddOrRemove(minus);

    //var val = document.getElementById('addProduct').value;
    //var numTransaction = document.getElementById('NumTransaction').value;

    ////var terminal = document.getElementById('tId').value;
    //var terminal = document.getElementById('TerminalId').value;

    //var minus = false;

    //var xhr = new XMLHttpRequest();
    //xhr.onreadystatechange = function () {
    //    if (this.readyState == 4 && this.status == 200) {
    //        document.getElementById('addProduct').value = "";
    //        document.getElementById('detail').innerHTML = xhr.responseText;
    //    }
    //}
    //////Get Method
    ////var url = "/Transaction/RefreshDetails?codeProduct=" + val
    ////          + "&numTransaction=" + numTransaction
    ////          + "&terminal=" + terminal;

    ////xhr.open("GET", url);
    ////xhr.send();

    ////Post Method
    //var url = "/Transaction/RefreshDetails";
    //var param = "codeProduct=" + val
    //    + "&numTransaction=" + numTransaction
    //    + "&terminal=" + terminal
    //    + "&minus=" + minus;
    //xhr.open("POST", url);
    //xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    //xhr.send(param);

}

function ButtonRemoveProduct_Click() {
    var minus = true;
    CreateRquestAddOrRemove(minus);

    //var val = document.getElementById('addProduct').value;
    //var numTransaction = document.getElementById('NumTransaction').value;            
    //var terminal = document.getElementById('TerminalId').value;
    ////var minus = true;
    //var xhr = new XMLHttpRequest();
    //xhr.onreadystatechange = function () {
    //    if (this.readyState == 4 && this.status == 200) {
    //        document.getElementById('addProduct').value = "";
    //        document.getElementById('detail').innerHTML = xhr.responseText;
    //    }
    //}

    ////Post Method
    //var url = "/Transaction/RefreshDetails";
    //var param = "codeProduct=" + val
    //    + "&numTransaction=" + numTransaction
    //    + "&terminal=" + terminal
    //    + "&minus=" + minus;
    //xhr.open("POST", url);
    //xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    //xhr.send(param);
}

function CreateRquestAddOrRemove(minus) {
    document.getElementById('errorAddProduct').textContent = "";
    document.getElementById('errorAddProduct').style.visibility = "hidden";
    try {
        var val = document.getElementById('addProduct').value;
        if (val === null || val === "") {
            //alert("Il faut saisir un produit");
            throw "Il faut saisir un produit";

        } else {
            var val = document.getElementById('addProduct').value;
            var numTransaction = document.getElementById('NumTransaction').value;
            var terminal = document.getElementById('TerminalId').value;

            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    document.getElementById('addProduct').value = "";
                    document.getElementById('detail').innerHTML = xhr.responseText;
                    var disc = document.getElementById('globalDiscount').value;
                    if (disc === "" || disc === null || disc === undefined) {
                        document.getElementById('GlobalTot').value = document.getElementById('subTotal1').value;
                    } else {
                        AddDiscount();
                    }
                }
            }

            //Post Method
            var url = "/Transaction/RefreshDetails";
            var param = "addProduct=" + val
                + "&numTransaction=" + numTransaction
                + "&terminalId=" + terminal
                + "&minus=" + minus;
            xhr.open("POST", url);
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.send(param);
        }
    } catch (e) {
        document.getElementById('errorAddProduct').textContent = e;
        document.getElementById('errorAddProduct').style.visibility = "visible";
        console.log(e);
    }
}

function AddDiscount() {
    //var subT = parseFloat(document.getElementById('subTotal2').value);
    //if (subT == 0 || subT == null) {
    //    subT = parseFloat(document.getElementById('subTotal1').value);
    //}
    document.getElementById('errorGlobalDiscount').textContent = "";
    document.getElementById('errorGlobalDiscount').style.visibility = "hidden";
    try {
        var subT = document.getElementById('subTotal1').value;
        subT = parseFloat(subT.replace(",", "."));
        var d = (parseFloat(document.getElementById('globalDiscount').value)) / 100;
        if (d < 0 || d > 1) {
            throw "valeur en % devant être comprise entre 0 et 100";
            //alert("valeur en % devant être comprise entre 0 et 100");  

        } else if (Number.isNaN(d) || d == undefined || d == null || d === "") {
            throw "valeur devant être un nombre entre 0 et 100";
            //alert("valeur devant être un nombre entre 0 et 100");
        } else if (d == 0) {
            document.getElementById('GlobalTot').value = subT;
        } else {
            var result = parseFloat(subT - (subT * d)).toFixed(2);
            document.getElementById('GlobalTot').value = result;
        }
    }
    catch (e) {
        document.getElementById('errorGlobalDiscount').textContent = e;
        document.getElementById('errorGlobalDiscount').style.visibility = "visible";
        console.log(e);
    }
}

//function AddVat() {
//	document.getElementById('errorAddVat').textContent = "";
//	document.getElementById('errorAddVat').style.visibility = "hidden";
//	try {
//		var v = document.getElementById('GlobalVAT').value;
//		var subT = document.getElementById('subTotal2').value;
//		//v = "0,21"  --> parseFloat(document.getElementById('listVats').value) ne marche pas
//		//le parseFloat donne 0, souci avec la ","  il faut la remplacer par "."
//		subT = parseFloat(subT.replace(",", "."));
//		if (v === "" || v === "0,00" || v === null) {
//			document.getElementById('GlobalTotal').value = subT;
//		} else {
//			v = parseFloat(v.replace(",", "."));
//			var result = parseFloat(subT * ++v).toFixed(2);
//			document.getElementById('GlobalTotal').value = result;
//		}
//	} catch (e) {
//		document.getElementById('errorAddVat').textContent = e;
//		document.getElementById('errorAddVat').style.visibility = "visible";
//		console.log(e);
//	}
//}

function SearchByCodeOrName() {
    document.getElementById('errorSearchProduct').textContent = "";
    document.getElementById('errorSearchProduct').style.visibility = "hidden";
    try {
        var val = document.getElementById('searchProduct').value;
        if (val === null || val === "") {
            //alert("Il faut saisir un produit");
            throw "Il faut saisir un produit";

        } else {
            //var val = document.getElementById('searchProduct').value;

            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    //document.getElementById('searchProduct').value = "";
                    document.getElementById('containerRight').innerHTML = xhr.responseText;
                }
            }

            //Post Method
            var url = "/Transaction/SearchProduct";
            var param = "Product=" + val;
            xhr.open("POST", url);
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.send(param);
        }
    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function AddItem(item) {
    document.getElementById('addProduct').value = item;
}

//function detail_Click(item) {
//    //todo
//}

function SearchBy(method) {
    var val = method.getAttribute('Value');
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('containerRight').innerHTML = xhr.responseText;
        }
    }

    //Post Method
    var url = "/Transaction/SearchBy";
    var param = "Method=" + val;
    xhr.open("POST", url);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(param);
}

function ProductBy(id, meth) {
    var val = id.getAttribute('Value');
    var method = meth;
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('containerRight').innerHTML = xhr.responseText;
        }
    }

    //Post Method
    var url = "/Transaction/ProductBy";
    var param = "Method=" + method
        + "&Argument=" + val;
    xhr.open("POST", url);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(param);
}

//function AddDiscount() {
//    //var subT = parseFloat(document.getElementById('subTotal2').value);
//    //if (subT == 0 || subT == null) {
//    //    subT = parseFloat(document.getElementById('subTotal1').value);
//    //}
//    document.getElementById('errorGlobalDiscount').textContent = "";
//    document.getElementById('errorGlobalDiscount').style.visibility = "hidden";
//    try {
//        var subT = document.getElementById('subTotal1').value;
//        subT = parseFloat(subT.replace(",", "."));
//        var d = (parseFloat(document.getElementById('globalDiscount').value)) / 100;
//        if (d < 0 || d > 1) {
//            throw "valeur en % devant être comprise entre 0 et 100";
//            //alert("valeur en % devant être comprise entre 0 et 100");  

//        } else if (Number.isNaN(d) || d == undefined || d == null || d === "") {
//            throw "valeur devant être un nombre entre 0 et 100";
//            //alert("valeur devant être un nombre entre 0 et 100");
//        } else if (d == 0) {
//            document.getElementById('subTotal2').value = subT;
//        } else {
//            var result = parseFloat(subT - (subT * d)).toFixed(2);
//            document.getElementById('subTotal2').value = result;
//        }
//    }
//    catch (e) {
//        document.getElementById('errorGlobalDiscount').textContent = e;
//        document.getElementById('errorGlobalDiscount').style.visibility = "visible";
//        console.log(e);
//    }
//}

//function Translate() {
//    var languageFrom = "fr";
//    var languageTo = "en";

//    if ($('#txtMsg').val().trim().length == 0) {
//        alert('Please enter text');
//        return false;
//    }

//    $("#msg").text('loading...');

//    $.ajax({
//        type: "GET",
//        url: 'api/values',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json"
//    }).done(function (token) {
//        var s = document.createElement("script");
//        s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=Bearer "
//            + token
//            + "&from="
//            + languageFrom
//            + "&to="
//            + languageTo
//            + "&text="
//            + $('#txtMsg').val();
//        document.getElementsByTagName("head")[0].appendChild(s);
//    }).fail(function (xhr, ajaxOptions, thrownError) {
//        alert(xhr.responseText);
//        $("#msg").text('Error');
//    });
//}

//window.mycallback = function (response) {
//    $("#msg").text('Translated Text: ' + response);
//}

//// remplacer par methodes controller
//function ButtonPayment_Click() {
//	var numTransaction = document.getElementById('NumTransaction').value;
//	var terminal = document.getElementById('TerminalId').value;
//	var vendor = document.getElementById('Vendor').value;
//	//var subTotitem = document.getElementById('subTotal1').value;
//	var discountG = document.getElementById('globalDiscount').value;
//	//var subTot = document.getElementById('subTotal2').value;
//	//var vat = document.getElementById('listVats').value;
//	var vat = document.getElementById('GlobalVAT').value;
//	var total = document.getElementById('GlobalTotal').value;

//	var xhr = new XMLHttpRequest();
//	xhr.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {
//            document.getElementById('page').innerHTML = xhr.response;
//            //document.getElementsByTagName('footer').setAttribute("visible", "hidden");
//		}
//	}

//	//Post Method
//	//var url = "/Transaction/PaymentTransaction";
//	var url = "/Transaction/Index";
//	var param = "numTransaction=" + numTransaction
//		+ "&terminalId=" + terminal
//		+ "&vendor=" + vendor
//		//+ "&subTotitem=" + subTotitem
//        + "&globalDiscount=" + discountG
//		//+ "&subTot=" + subTot
//		+ "&globalVAT=" + vat
//		+ "&globalTotal=" + total;
//	xhr.open("POST", url);
//	xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//	xhr.send(param);
//}

//function ButtonCancel_Click() {

//}

//function CancelTransac() {
//    var numTransaction = document.getElementById('NumTransaction').value;
//    var xhr = new XMLHttpRequest();

//    //Post Method
//    var url = "/Transaction/CancelTransaction";
//    var param = "numTransaction=" + numTransaction;
//    xhr.open("POST", url);
//    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//    xhr.send(param);
//}

//function methodPayment(id) {
//    var val = id.getAttribute('Value');
//    var numTransaction = document.getElementById('NumTransaction').value;
//    var total = document.getElementById('GlobalTotal').value;

//    var xhr = new XMLHttpRequest();
//    xhr.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {

//            var choice = document.getElementById('paymentChoice');
//            //choice.setAttribute("visibility", "visible");
//            choice.innerHTML = xhr.responseText;

//            //switch (val) {
//            //    case "Cash":
//            //        document.getElementById('pCash').setAttribute("visibility", "visible");
//            //        //choice.setAttribute("visibility", "visible");
//            //        document.getElementById('pCash').innerHTML = xhr.responseText;
//            //        break;
//            //    case "Card":
//            //        var choice = document.getElementById('pCard');
//            //        choice.setAttribute("visibility", "visible");
//            //        choice.innerHTML = xhr.responseText;
//            //        break;
//            //    case "Voucher":
//            //        var choice = document.getElementById('pVoucher');
//            //        choice.setAttribute("visibility", "visible");
//            //        choice.innerHTML = xhr.responseText;
//            //        break;
//            //    default:
//            //        document.getElementById('paymentChoice').innerHTML = xhr.responseText;
//            //        break;
//            //}

//        }
//    }
//    //Post Method
//    var url = "/Pay/MethodChoice";
//    var param = "numTransaction=" + numTransaction
//        + "&globalTotal=" + total
//        + "&methodP=" + val;
//    xhr.open("POST", url);
//    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//    xhr.send(param);
//}

//function AddCash() {
//    var cash = document.getElementById('CashVal').value;
//    var total = document.getElementById('GlobalTotal').value;
//    var transac = document.getElementById('NumTransaction').value;
//    //document.getElementById('GTotal').value = "";
//    var xhr = new XMLHttpRequest();
//    xhr.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {
//            document.getElementById('totalToPay').style.visibility = "hidden";            
//            document.getElementById('paymentChoice').innerHTML = xhr.responseText;
//            var test = document.getElementById('vBagAmount').value;
//            document.getElementById('TTPay').style.visibility = "visible";
//            if (test === "0") {
//                document.getElementById('vBagAmount').style.borderColor = "green";
//                document.getElementById('CashReturn').style.borderColor = "orange";
//            }


//        }
//    }
//    //Post Method
//    var url = "/Pay/PayCash";
//    var param = "cashVal=" + cash
//        + "&numTransaction=" + transac
//        + "&amount=" + total;
//    xhr.open("POST", url);
//    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//    xhr.send(param);
//}

//function AskValidationCard() {
//    var total = document.getElementById('GlobalTotal').value;
//    var transac = document.getElementById('NumTransaction').value;
//    var xhr = new XMLHttpRequest();
//    xhr.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {

//            document.getElementById('paymentChoice').innerHTML = xhr.responseText;
//            var test = document.getElementById('vBagAmount').value;
//            document.getElementById('TTPay').style.visibility = "visible";
//            if (test === "0") {
//                document.getElementById('totalToPay').style.visibility = "hidden"; 
//                document.getElementById('vBagAmount').style.borderColor = "green";
//                document.getElementById('CashReturn').style.borderColor = "orange";
//            }
//        }
//    }
//    //Post Method
//    var url = "/Pay/PayCard";
//    var param = "numTransaction=" + transac
//        + "&amount=" + total;
//    xhr.open("POST", url);
//    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//    xhr.send(param);
//}

////simulation reponse bank
//function AskValidationCard() {
//    return randomInt(0, 1);
//}

////choix aléatoire 0 ou 1
//function randomInt(min, max) {
//    return Math.floor(Math.random() * (max - min + 1)) + min;
//}

//function AddCash() {
//    var cash = document.getElementById('cashVal').value;
//    var total = document.getElementById('GlobalTotal').value;
//    total = parseFloat(total.replace(",", "."));
//    cash = parseFloat(cash.replace(",", "."));

//    if (cash > total) {
//        var result = parseFloat(cash - total).toFixed(2);
//        document.getElementById('cashReturn').value = result;
//        document.getElementById('cashReturn').style.borderColor = "orange";
//        document.getElementById('GlobalTotal').value = 0;
//        document.getElementById('GlobalTotal').style.borderColor = "green";
//    } else if (cash < total) {
//        var result = parseFloat(total - cash).toFixed(2);
//        document.getElementById('GlobalTotal').value = result;
//    } else {
//        document.getElementById('cashReturn').value = 0;
//        document.getElementById('cashReturn').style.borderColor = "green";
//        document.getElementById('GlobalTotal').value = 0;
//        document.getElementById('GlobalTotal').style.borderColor = "green";
//    }

//}

//function AddSubTot() {
//    var subT = parseFloat(document.getElementById('subTotal2').value).toFixed(2);
//    document.getElementById('TotalEnd').value = subT
//    //return subT;
//}

//function CloneValue(c) {
//    //var c = parseFloat(document.getElementById('subTotal1').value);
//    document.getElementById('subTotal2').value = c;
//}

//function AddVoucher() {
//    var subT = parseFloat(document.getElementById('subTotal2').value);
//    if (subT == 0 || subT == null) {
//        subT = parseFloat(document.getElementById('subTotal1').value);
//    }
//    var v = parseFloat(document.getElementById('voucher').value);
//    var temp = 0;
//    if (subT > v || subT < v) {
//        temp = subT - v;
//    }
//    document.getElementById('subTotal2').value = temp;
//}

//function AddVoucher() {
//    var v = parseFloat(document.getElementById('voucher').value);
//    var subT = parseFloat(document.getElementById('subTotal1').value);

//    var xhr = new XMLHttpRequest();
//    xhr.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {

//            document.getElementById('subTotal2').innerHTML = xhr.responseText;
//        }
//    }

//    //Post Method
//    var url = "/Transaction/AddVoucher";
//    var param = "voucher=" + v
//        + "&subTotal=" + subT;

//    xhr.open("POST", url);
//    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//    xhr.send(param);
//}


//function AddAllItemSubTot() {
//    var tot;
//    var p = parseFloat(document.getElementById('itemSubTot').value).toFixed(2);
//    if (p == null || p == 0 || p == "-") {
//        tot = 0;
//    } else {
//        tot += p;
//        document.getElementById('subTotal1').value = tot;
//    }
//}

//function getItemSubTot() {
//    var p = parseFloat(document.getElementById('price').value).toFixed(2);
//    var q = parseFloat(document.getElementById('quantity').value).toFixed(2);
//    var d = parseFloat(document.getElementById('Discount').value).toFixed(2);
//    alert("p= " + p + "\nq= " + q + "\nd= " + d);
//    if (p == null || p == 0 || q == null || q == 0) {
//        document.getElementById('itemSubTot').value = 0;
//    } else if (d == 0 || d == null) {
//        document.getElementById('itemSubTot').value = (p * q);
//    } else {
//        //ex: (100*2)*0.05 = 10
//        var temp = (p * q) * d;
//        document.getElementById('itemSubTot').value = (p * q) - temp;
//    }
//}