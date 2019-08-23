// find elements


function addRow(i) {
    document.querySelector("#content").insertAdjacentHTML(
        'afterbegin',
        `<div class="row">
      <input type="text" name="name" id="ns`+ i + `"" value="" />
      <label><input type="checkbox" name="check" value="1" />Checked?</label>
      <input type="button" value="-" onclick="removeRow(this)">
    </div>`
    )
}

var strip = function (s) { return s.replace(/^\s*|\s*$/g, ''); }

//Add paste listener. In a production environment this would of course
//need the proper checks to see what's supported, old IE versions, blah blah
document.getElementById('ns1').addEventListener('paste', function (e) {
    var lines = strip(e.clipboardData.getData('Text')).split(/\r?\n/);
    if (lines.length == 1) {
        return;
    }
    for (var i = 1; i <= lines.length; i++) {
        if (i > 1) addRow(i)
        var el = document.getElementById('ns' + i);
        el.value = strip(lines[i - 1] || '');


    }
    e.preventDefault();
});