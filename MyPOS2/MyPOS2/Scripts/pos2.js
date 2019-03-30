
/* ========================================================================
 * Scripts javascript for 
 * MyPOS
 * ======================================================================== */

/*
 * Scripts for
 * Transaction views
* ======================================================================== */

function goBack() {
    window.history.go(-1);
}

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
}

function ButtonRemoveProduct_Click() {
    var minus = true;
    CreateRquestAddOrRemove(minus);
}

function CreateRquestAddOrRemove(minus) {
    document.getElementById('errorAddProduct').textContent = "";
    document.getElementById('errorAddProduct').style.visibility = "hidden";
    try {
        var val = document.getElementById('addProduct').value;
        if (val === null || val === "") {
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
    document.getElementById('errorGlobalDiscount').textContent = "";
    document.getElementById('errorGlobalDiscount').style.visibility = "hidden";
    try {
        var subT = document.getElementById('subTotal1').value;
        subT = parseFloat(subT.replace(",", "."));
        var d = (parseFloat(document.getElementById('globalDiscount').value)) / 100;
        if (d < 0 || d > 1) {
            throw "valeur en % devant être comprise entre 0 et 100";
        } else if (Number.isNaN(d) || d == undefined || d == null || d === "") {
            throw "valeur devant être un nombre entre 0 et 100";
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

function SearchByCodeOrName() {
    document.getElementById('errorSearchProduct').textContent = "";
    document.getElementById('errorSearchProduct').style.visibility = "hidden";
    try {
        var val = document.getElementById('searchProduct').value;
        if (val === null || val === "") {
            throw "Il faut saisir un produit";
        } else {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
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

function SearchAllProduct() {
    try {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('containerRight').innerHTML = xhr.responseText;
            }
        }

        //Get Method
        var url = "/Transaction/SearchAllProduct" 
	    xhr.open("GET", url);
	    xhr.send();


    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function AddItem(item) {
    document.getElementById('addProduct').value = item;
}

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

function SearchHostName() {
    try {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('nameId').innerHTML = xhr.responseText;
                var T = document.getElementById('detail').textContent;
                document.getElementById('nameTerminal').value = T.trim();
                
            }
        }

        //Get Method
        var url = "/Terminals/SearchHostName"
        xhr.open("GET", url);
        xhr.send();
    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function ChangeLanguageTicket() {
    try {
        var val = document.getElementById('Language').value;
        var transac = document.getElementById('NumTransaction').value;
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('myTicket').innerHTML = xhr.responseText;
                

            }
        }

        //Post Method
        var url = "/Pay/ChangeLanguageTicket";
        var param = "Language=" + val
            + "&NumTransaction=" + transac;
        xhr.open("POST", url);
        xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhr.send(param);
    } catch (e) {
        document.getElementById('errorSearchProduct').textContent = e;
        document.getElementById('errorSearchProduct').style.visibility = "visible";
        console.log(e);
    }
}

function FctPrint(divName) {
    var printContents = document.getElementById(divName).innerHTML;
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;

    window.print();

    document.body.innerHTML = originalContents;
}
