// ===============================
// AUTO BARCODE GENERATOR
// ===============================
function generateBarcode() {
    let barcode = '';
    for (let i = 0; i < 12; i++) {
        barcode += Math.floor(Math.random() * 10);
    }
    document.getElementById("barcodeInput").value = barcode;
}

// ===============================
// IMAGE PREVIEW
// ===============================
function previewImage(event) {
    let output = document.getElementById("imagePreview");
    if (event.target.files && event.target.files[0]) {
        // Fast preview using ObjectURL
        output.src = URL.createObjectURL(event.target.files[0]);
    }
}

// ===============================
// CATEGORY → SUBCATEGORY DEPENDENT
// ===============================
$(document).ready(function () {
    $("#StockCategoryId").change(function () {
        let categoryId = $(this).val();
        let subDropdown = $("#StockSubCategoryId");

        subDropdown.empty().append('<option>Loading...</option>');

        if (!categoryId) {
            subDropdown.empty().append('<option value="">-- Select Sub Category --</option>');
            return;
        }

        $.ajax({
            url: '/Product/GetSubCategories',
            type: 'GET',
            data: { categoryId: categoryId },
            success: function (data) {
                subDropdown.empty();
                subDropdown.append('<option value="">-- Select Sub Category --</option>');
                $.each(data, function (i, item) {
                    subDropdown.append(
                        `<option value="${item.stockSubCategoryId}">${item.subCategoryName}</option>`
                    );
                });
            },
            error: function (xhr, status, error) {
                subDropdown.empty().append('<option value="">-- Failed to load --</option>');
                console.error("Error fetching subcategories:", error);
            }
        });
    });
});