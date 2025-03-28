function aplicarMascaraTelefone(input) {
    input.addEventListener('input', function (e) {
        let x = e.target.value.replace(/\D/g, '').slice(0, 11);
        let formatted = x;

        if (x.length >= 2) {
            formatted = `(${x.slice(0, 2)}`;
        }
        if (x.length >= 7) {
            formatted += `) ${x.slice(2, 7)}-${x.slice(7)}`;
        } else if (x.length > 2) {
            formatted += `) ${x.slice(2)}`;
        }

        e.target.value = formatted;
    });
}
