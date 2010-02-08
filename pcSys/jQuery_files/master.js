// JScript File
//Gabriela Paredes

function irTab1() {
    //La pagina donde lleva el link
    window.location.href = 'Proveedor.aspx';
    //Activa la pestanha correspondiente
    document.getElementById('tab1').setAttribute('class', 'tab active');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    document.getElementById('tab5').setAttribute('class', 'tab');
    return false;
}

function irTab2() {
    //La pagina donde lleva el link
    //window.location.href = 'pruebaMaster2.aspx';
    window.location.href = 'Compras.aspx';
    //Activa la pestanha correspondiente
    document.getElementById('tab2').setAttribute('class', 'tab active');
    document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    document.getElementById('tab5').setAttribute('class', 'tab');
    return false;
    }

function irTab3() {
     //La pagina donde lleva el link
    window.location.href = 'Pagos.aspx';
    //Activa la pestanha correspondiente
    document.getElementById('tab3').setAttribute('class', 'tab active');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    document.getElementById('tab5').setAttribute('class', 'tab');
    return false;
}

function irTab4() {
     //La pagina donde lleva el link
    window.location.href = 'NotaCredito.aspx';
    //Activa la pestanha correspondiente
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab active');
    document.getElementById('tab5').setAttribute('class', 'tab');
    return false;
}

function irTab5() {
     //La pagina donde lleva el link
    window.location.href = 'ReporteFaturas.aspx';
    //Activa la pestanha correspondiente
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    document.getElementById('tab5').setAttribute('class', 'tab active');
    return false;
}

function irTab5() {
     //La pagina donde lleva el link
    window.location.href = 'reporteNotaCredito.aspx';
    //Activa la pestanha correspondiente
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    document.getElementById('tab5').setAttribute('class', 'tab active');
    return false;
}
