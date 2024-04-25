const bookStoreCartKey = "BookStoreCart";

function getOrderDataFromLocalStorage() {
    var cartData = localStorage.getItem(bookStoreCartKey);
    if (cartData) {
        var orderProducts = JSON.parse(cartData);
        return orderProducts;
    } else {
        return [];
    }
}

function updateLocalStorage(cartItems) {
    localStorage.setItem(bookStoreCartKey, JSON.stringify(cartItems));
}

function addToCart(productId, quantity) {
    var cartData = localStorage.getItem(bookStoreCartKey);
    var cartItems = cartData ? JSON.parse(cartData) : [];
    var existingItem = cartItems.find(item => item.prodId === productId);
    if (existingItem) {
        existingItem.Quantity += quantity;
    } else {
        cartItems.push({ prodId: productId, quantity: quantity });
    }

    updateLocalStorage(cartItems);
}

function removeFromCart(productId) {
    var cartData = localStorage.getItem(bookStoreCartKey);
    var cartItems = cartData ? JSON.parse(cartData) : [];
    cartItems = cartItems.filter(item => item.prodId !== productId);
    updateLocalStorage(cartItems);
}

function updateQuantity(productId, newQuantity) {
    var cartData = localStorage.getItem(bookStoreCartKey);
    var cartItems = cartData ? JSON.parse(cartData) : [];
    var itemToUpdate = cartItems.find(item => item.prodId === productId);
    if (itemToUpdate) {
        itemToUpdate.quantity = newQuantity;
    }
    updateLocalStorage(cartItems);
    updateCartItemUI(productId, cartItem.quantity);
}

function increaseQuantity(productId) {
    var cartData = localStorage.getItem(bookStoreCartKey);
    var cartItems = cartData ? JSON.parse(cartData) : [];
    var itemToUpdate = cartItems.find(item => item.prodId === productId);
    if (itemToUpdate) {
        itemToUpdate.quantity++;
    }
    updateLocalStorage(cartItems);
    updateCartItemUI(productId, itemToUpdate.quantity);
}

function decreaseQuantity(productId) {
    var cartData = localStorage.getItem(bookStoreCartKey);
    var cartItems = cartData ? JSON.parse(cartData) : [];
    var itemToUpdate = cartItems.find(item => item.prodId === productId);
    if (itemToUpdate && itemToUpdate.quantity > 1) {
        itemToUpdate.quantity--;
    }
    updateLocalStorage(cartItems);
    updateCartItemUI(productId, itemToUpdate.quantity);
}

function updateCartItemUI(productId, newQuantity) {
    const quantityInput = document.querySelector(`input[data-product-id="${productId}"]`);
    if (quantityInput) {
        quantityInput.value = newQuantity;
    }
}


async function renderCartItems() {
    var cartData = getOrderDataFromLocalStorage();
    var cartItemsContainer = document.getElementById('cartItems');
    // Clear existing cart items
    cartItemsContainer.innerHTML = '';
    // Loop through cart items and create table rows
    for(const item of cartData) {
        var row = document.createElement('tr');

        const response = await fetch(`/Order/CartBookDetail?pubID=${item.prodId}`);
        if (!response.ok) {
            console.error('Failed to fetch book details:', response.status);
            continue;
        }

        const bookData = await response.json();
        row.innerHTML = `
                <td>
                    <div class="d-flex align-items-center">
                        <img src="${bookData.imgSrc}" class="img-fluid rounded-3" style="width: 120px;" alt="Book">
                        <div class="flex-column ms-4">
                            <p class="mb-2">${bookData.bookName}</p>
                            <p class="mb-0">${bookData.author}</p>
                        </div>
                    </div>
                </td>
                <td>
                    <div class="d-flex flex-row">
                        <button class="btn btn-link px-2" onclick="decreaseQuantity('${bookData.pubId}')">
                            <i class="fas fa-minus"></i>
                        </button>
                        <input type="number" class="form-control form-control-sm" data-product-id="${bookData.pubId}" value="${item.quantity}" readonly>
                        <button class="btn btn-link px-2" onclick="increaseQuantity('${bookData.pubId}')">
                            <i class="fas fa-plus"></i>
                        </button>
                    </div>
                </td>
                <td>${bookData.price}</td>
                <td>
                    <button class="btn" onclick="removeFromCart('${bookData.pubId}')">
                        <i class="fa-solid fa-trash-can"></i>
                        Remove
                    </button>
                </td>
            `;
        cartItemsContainer.appendChild(row);
    };
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

async function checkout() {
    const name = document.getElementById('name').value;
    const surname = document.getElementById('surname').value;
    const address = document.getElementById('address').value;
    const phone = document.getElementById('phone').value;

    const orderProducts = getOrderDataFromLocalStorage();

    const order = {
        orderID: uuidv4(),
        name: name,
        surname: surname,
        address: address,
        phone: phone,
        orderProducts: orderProducts
    };

    try {
        const response = await fetch('/Order/ReceiveOrder/', {
            method: 'POST',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            body: JSON.stringify(order)
        });

        if (!response.ok) {
            throw new Error('Failed to submit order');
        }

        localStorage.removeItem(bookStoreCartKey);

        console.log('Order submitted successfully');
        window.location.href = '/home/index';
    } catch (error) {
        console.error('Error submitting order:', error.message);
    }
}

function showStep(step) {
    document.querySelectorAll('[id^="step"]').forEach(stepElement => {
        stepElement.style.display = 'none';
    });
    document.getElementById(`step${step}`).style.display = 'block';
}

renderCartItems();