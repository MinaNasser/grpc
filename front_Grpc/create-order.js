// import { PaymentRequest } from './src/app/generated/src/app/protos/payment_pb.js';
// import { PaymentClient } from './src/app/generated/src/app/protos//payment_pb_service.js';

// // add inventory_pb_service.js 
// import { InventoryClient } from './src/app/generated/src/app/protos/inventory_pb_service.js';
// import { InventoryRequest } from './src/app/generated/src/app/protos/inventory_pb.js';



 
window.onload = function() {
    fetchProducts();
};

// تحميل قائمة المنتجات
function fetchProducts() {
    fetch('http://localhost:5121/api/order/products')  // استدعاء API لعرض المنتجات
        .then(response => response.json())
        .then(data => {
            const productTableBody = document.querySelector('#productTable tbody');
            productTableBody.innerHTML = '';  // إعادة تعيين الجدول

            // إضافة كل منتج إلى الجدول
            data.forEach((product, index) => {
                const row = productTableBody.insertRow();
                row.insertCell(0).innerText = index + 1;
                row.insertCell(1).innerText = product.name;
                row.insertCell(2).innerText = `$${product.price.toFixed(2)}`;
                row.insertCell(3).innerText = product.stock;
                row.insertCell(4).innerHTML = `<button onclick="addProduct(${product.id}, '${product.name}', ${product.price}, ${product.stock})">Add</button>`;
            });
        })
        .catch(error => console.error('Error fetching products:', error));
}

// تعريف المتغير لتتبع رقم المنتج
let productId = 1;

// دالة لإضافة المنتج إلى الجدول
function addProduct() {
    const productName = document.getElementById("productName").value;
    const productQty = parseInt(document.getElementById("productQty").value);
    const productPrice = parseFloat(document.getElementById("productPrice").value);
    
    // تحقق من وجود قيم صحيحة للمنتج
    if (productName && productQty > 0 && !isNaN(productPrice) && productPrice > 0) {
        const productTable = document.getElementById("productTable").getElementsByTagName('tbody')[0];
        const newRow = productTable.insertRow();

        // إضافة البيانات في الخلايا
        newRow.innerHTML = `
            <td>${productId++}</td>
            <td>${productName}</td>
            <td>${productQty}</td>
            <td>$${productPrice.toFixed(2)}</td>
            <td>$${(productQty * productPrice).toFixed(2)}</td>
            <td><button type="button" onclick="removeProduct(this)">Remove</button></td>
        `;

        // تحديث المجموع الكلي
        updateTotal();
    } else {
        alert("Please enter valid product details.");
    }
}

// دالة لحساب المجموع الكلي
function updateTotal() {
    let total = 0;
    const productTable = document.getElementById("productTable").getElementsByTagName('tbody')[0];
    const rows = productTable.getElementsByTagName('tr');

    for (let row of rows) {
        const totalCell = row.cells[4]; // عمود الإجمالي
        total += parseFloat(totalCell.textContent.replace('$', ''));
    }

    document.getElementById("orderTotal").textContent = `Total: $${total.toFixed(2)}`;
}

// دالة لإزالة المنتج من الجدول
function removeProduct(button) {
    const row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
    updateTotal();
}


// فحص المخزون
function checkProductStock(productId, quantity, availableStock) {
    if (quantity > availableStock) {
        alert('Not enough stock available!');
    } else {
        // إذا كان المخزون كافٍ، أضف المنتج إلى الطلب
        const row = document.getElementById('productTable').insertRow();
        row.insertCell(0).innerText = document.querySelectorAll('#productTable tr').length; // الرقم التسلسلي
        row.insertCell(1).innerText = productName;
        row.insertCell(2).innerText = quantity;
        row.insertCell(3).innerText = `$${productPrice.toFixed(2)}`;
        row.insertCell(4).innerText = `$${(productPrice * quantity).toFixed(2)}`;
        row.insertCell(5).innerHTML = `<button onclick="removeProduct(this)">Remove</button>`;
        
        updateTotal();
    }
}


// إرسال الطلب
function submitOrder() {
    const customerName = document.getElementById('customerName').value;
    const customerPhone = document.getElementById('customerPhone').value;
    const customerEmail = document.getElementById('customerEmail').value;
    const paymentMethod = document.getElementById('paymentMethod').value;

    const items = [];
    const rows = document.querySelectorAll('#productTable tr');
    rows.forEach(row => {
        const productName = row.cells[1]?.innerText;  // اسم المنتج
        const productPrice = parseFloat(row.cells[3]?.innerText.replace('$', '')); // السعر
        const quantity = parseInt(row.cells[2]?.innerText);  // الكمية
        if (productName && productPrice && quantity) {
            items.push({ productName, productPrice, quantity });
        }
    });

    const order = {
        customerName,
        customerPhone,
        customerEmail,
        paymentMethod,
        items
    };

    // استدعاء خدمة الدفع
    processPayment(order);
}

// إجراء الدفع باستخدام gRPC
function processPayment(order) {
    const paymentRequest = new src_app_protos_payment_pb.PaymentRequest();
    paymentRequest.setAmount(calculateTotalAmount(order.items)); // حساب المبلغ الإجمالي
    paymentRequest.setPaymentMethod(order.paymentMethod);

    const paymentClient = new PaymentClient("http://localhost:5000", {}); // استبدل بالعنوان الصحيح

    paymentClient.processPayment(paymentRequest, {}, (error, response) => {
        if (error) {
            console.error("Error processing payment:", error);
            alert("Payment failed!");
        } else {
            alert("Payment successful!");
            // إرسال الطلب إذا تم الدفع بنجاح
            sendOrderToServer(order);
        }
    });
}

// حساب المبلغ الإجمالي
function calculateTotalAmount(items) {
    return items.reduce((total, item) => total + (item.productPrice * item.quantity), 0);
}

// إرسال الطلب إلى الخادم بعد الدفع
function sendOrderToServer(order) {
    fetch('/api/order/create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(order)
    })
    .then(response => response.json())
    .then(data => {
        alert('Order Created Successfully!');
        console.log('Order Response:', data);
    })
    .catch(error => console.error('Error creating order:', error));
}
