function generatePDF() {
	const element = document.getElementById("pdf");
	var filename = new Date().toDateString() + '.pdf';
	html2pdf().from(element).set({
		margin: 0,
		filename: filename,
		jsPDF: {
			orientation: 'portrait',
			unit: 'in',
			format: 'a4',
			compressPDF: true
		}
	})
	.save();
}