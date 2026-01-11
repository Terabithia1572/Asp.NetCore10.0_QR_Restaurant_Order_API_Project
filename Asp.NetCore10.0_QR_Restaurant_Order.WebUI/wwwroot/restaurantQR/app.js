/* app.js */
document.addEventListener('DOMContentLoaded', () => {
    
    // 1) Navbar Scroll Effect (Enhanced Glassmorphism)
    const mainNav = document.getElementById('mainNav');
    let lastScroll = 0;
    
    window.addEventListener('scroll', () => {
        const currentScroll = window.scrollY;
        
        if (currentScroll > 50) {
            mainNav.classList.add('scrolled');
        } else {
            mainNav.classList.remove('scrolled');
        }
        
        // Smooth opacity transition
        if (currentScroll > lastScroll && currentScroll > 100) {
            mainNav.style.opacity = '0.98';
        } else {
            mainNav.style.opacity = '1';
        }
        
        lastScroll = currentScroll;
    }, { passive: true });

    // 2) Reservation Form Logic
    const reservationForm = document.getElementById('reservationForm');
    if (reservationForm) {
        reservationForm.addEventListener('submit', (e) => {
            e.preventDefault();
            
            // Success Animation
            const submitBtn = reservationForm.querySelector('button[type="submit"]');
            const originalText = submitBtn.innerHTML;
            
            submitBtn.disabled = true;
            submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm"></span> İşleniyor...';

            setTimeout(() => {
                const modal = bootstrap.Modal.getInstance(document.getElementById('reserveModal'));
                modal.hide();
                
                showToast('Rezervasyonunuz başarıyla alındı. Teşekkürler!', 'success');
                
                submitBtn.disabled = false;
                submitBtn.innerHTML = originalText;
                reservationForm.reset();
            }, 2000);
        });
    }

// 3) Mini Cart (Demo)
const cartToast = document.getElementById('cartToast');
const toastMsg = document.getElementById('toastMsg');

const cartCountEl = document.getElementById('cartCount');
const cartItemsEl = document.getElementById('cartItems');
const cartSubtotalEl = document.getElementById('cartSubtotal');
const cartServiceEl = document.getElementById('cartService');
const cartTotalEl = document.getElementById('cartTotal');

const clearCartBtn = document.getElementById('clearCartBtn');
const checkoutBtn = document.getElementById('checkoutBtn');

let cart = []; // {name, price, img, qty}

function formatTL(n) {
    return `${Math.round(n)} TL`;
}

function calcTotals() {
    const subtotal = cart.reduce((acc, x) => acc + (x.price * x.qty), 0);
    const service = subtotal > 0 ? Math.max(15, subtotal * 0.05) : 0; // demo servis
    const total = subtotal + service;

    cartSubtotalEl.innerText = formatTL(subtotal);
    cartServiceEl.innerText = formatTL(service);
    cartTotalEl.innerText = formatTL(total);

    const count = cart.reduce((acc, x) => acc + x.qty, 0);
    cartCountEl.innerText = count;
}

function renderCart() {
    if (!cartItemsEl) return;

    if (cart.length === 0) {
        cartItemsEl.innerHTML = `
            <div class="cart-empty text-center py-5">
                <i class="bi bi-emoji-smile fs-1 text-emerald"></i>
                <h6 class="fw-bold mt-3">Sepet boş</h6>
                <p class="text-gray-400 mb-0">Menüden ürün ekleyerek siparişe başlayabilirsin.</p>
            </div>
        `;
        calcTotals();
        return;
    }

    cartItemsEl.innerHTML = cart.map((item, idx) => `
        <div class="cart-item">
            <img class="cart-item-img" src="${item.img}" alt="item">
            <div class="flex-grow-1">
                <p class="cart-item-title">${item.name}</p>
                <p class="cart-item-meta">${formatTL(item.price)} • ${item.qty} adet</p>

                <div class="d-flex align-items-center gap-2 mt-2">
                    <button class="qty-btn" data-action="dec" data-idx="${idx}">
                        <i class="bi bi-dash"></i>
                    </button>
                    <span class="qty-num">${item.qty}</span>
                    <button class="qty-btn" data-action="inc" data-idx="${idx}">
                        <i class="bi bi-plus"></i>
                    </button>

                    <button class="btn btn-sm btn-link text-danger ms-auto text-decoration-none" data-action="remove" data-idx="${idx}">
                        <i class="bi bi-trash3"></i> Sil
                    </button>
                </div>
            </div>
        </div>
    `).join('');

    // actions
    cartItemsEl.querySelectorAll('[data-action]').forEach(btn => {
        btn.addEventListener('click', () => {
            const action = btn.getAttribute('data-action');
            const idx = parseInt(btn.getAttribute('data-idx'));

            if (Number.isNaN(idx)) return;

            if (action === 'inc') cart[idx].qty++;
            if (action === 'dec') cart[idx].qty = Math.max(1, cart[idx].qty - 1);
            if (action === 'remove') cart.splice(idx, 1);

            renderCart();
        });
    });

    calcTotals();
}

// Menüden ekleme (data-name var, buradan img + price da alıyoruz)
document.querySelectorAll('.btn-add-cart').forEach(btn => {
    btn.addEventListener('click', function() {
        const name = this.getAttribute('data-name') || 'Ürün';
        const row = this.closest('.menu-item-row');

        // img
        const img = row ? row.querySelector('img')?.getAttribute('src') : 'https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=200&h=200&fit=crop';

        // fiyat parse (örn: "295 TL")
        let price = 199;
        if (row) {
            const priceText = row.querySelector('.text-emerald.fw-bold')?.innerText || '';
            const onlyNum = priceText.replace(/[^\d]/g, '');
            if (onlyNum) price = parseInt(onlyNum);
        }

        const existing = cart.find(x => x.name === name);
        if (existing) existing.qty++;
        else cart.push({ name, price, img, qty: 1 });

        renderCart();
        showToast(`${name} sepete eklendi!`);
    });
});

if (clearCartBtn) {
    clearCartBtn.addEventListener('click', () => {
        cart = [];
        renderCart();
        showToast('Sepet temizlendi.');
    });
}

if (checkoutBtn) {
    checkoutBtn.addEventListener('click', () => {
        if (cart.length === 0) {
            showToast('Sepet boş. Önce ürün ekle.');
            return;
        }
        showToast('Demo: Sipariş alındı! (API ile bağlanacak)');
    });
}

function showToast(message) {
    toastMsg.innerText = message;
    const toast = new bootstrap.Toast(cartToast);
    toast.show();
}

// init
if (cartCountEl && cartItemsEl) renderCart();


    // 4) Intersection Observer for Animations (Fade-in effect)
    const observerOptions = {
        threshold: 0.1
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-in');
                observer.unobserve(entry.target);
            }
        });
    }, observerOptions);


    // CSS injection for observer animation
    const style = document.createElement('style');
    style.innerHTML = `
        .animate-in {
            opacity: 1 !important;
            transform: translateY(0) !important;
        }
    `;
    document.head.appendChild(style);

    // 4) Premium Scroll Reveal (stagger with scale)
const revealEls = document.querySelectorAll(
  'section .container, .dish-card, .menu-item-row, .card-discount, .work-card, .experience-badge, .map-placeholder, .chef-card, .testimonial-card, .stat-card, .stat-wide'
);

revealEls.forEach((el, i) => {
  el.classList.add('reveal');
  // doğal stagger: her 3 elemanda bir reset gibi hissedilecek
  const mod = i % 3;
  if (mod === 0) el.classList.add('reveal-delay-1');
  if (mod === 1) el.classList.add('reveal-delay-2');
  if (mod === 2) el.classList.add('reveal-delay-3');
});

const revealObserver = new IntersectionObserver((entries) => {
  entries.forEach(entry => {
    if (entry.isIntersecting) {
      entry.target.classList.add('reveal-in');
      revealObserver.unobserve(entry.target);
    }
  });
}, { threshold: 0.08 });

revealEls.forEach(el => revealObserver.observe(el));
// Menu tab switch animation
document.querySelectorAll('#menuTabs button[data-bs-toggle="pill"]').forEach(btn => {
  btn.addEventListener('shown.bs.tab', () => {
    const activePane = document.querySelector('#menuContent .tab-pane.active');
    if (!activePane) return;
    activePane.classList.remove('reveal-in');
    activePane.classList.add('reveal');
    requestAnimationFrame(() => {
      activePane.classList.add('reveal-in');
    });
  });
});

// 6) Testimonials Slider (Swiper) - Premium Smooth
if (document.querySelector('.testimonialSwiper')) {
    new Swiper('.testimonialSwiper', {
        slidesPerView: 1,
        spaceBetween: 24,
        loop: true,
        speed: 1200,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false
        },
        pagination: {
            el: '.testimonialSwiper .swiper-pagination',
            clickable: true
        },
        navigation: {
            nextEl: '.swiper-btn-next',
            prevEl: '.swiper-btn-prev'
        },
        effect: 'slide',
        fadeEffect: {
            crossFade: true
        },
        breakpoints: {
            768: { slidesPerView: 2, spaceBetween: 24 },
            1200: { slidesPerView: 3, spaceBetween: 24 }
        }
    });
}
// 6) Counter Animation (stats)
const counterEls = document.querySelectorAll('.counter');
const counterDecimalEls = document.querySelectorAll('.counter-decimal');

function animateCounter(el, target, isDecimal = false) {
    const duration = 1200;
    const start = 0;
    const startTime = performance.now();

    function tick(now) {
        const progress = Math.min((now - startTime) / duration, 1);
        let val = start + (target - start) * progress;

        if (isDecimal) {
            el.innerText = val.toFixed(1);
        } else {
            el.innerText = Math.floor(val);
        }

        if (progress < 1) requestAnimationFrame(tick);
    }

    requestAnimationFrame(tick);
}

const statsSection = document.getElementById('stats');
if (statsSection) {
    const statsObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (!entry.isIntersecting) return;

            counterEls.forEach(el => animateCounter(el, parseInt(el.dataset.target || "0"), false));
            counterDecimalEls.forEach(el => animateCounter(el, parseFloat(el.dataset.target || "0"), true));

            statsObserver.disconnect();
        });
    }, { threshold: 0.2 });

    statsObserver.observe(statsSection);
}
// 7) Scroll-to-top
const scrollTopBtn = document.getElementById('scrollTopBtn');

function toggleScrollTop() {
    if (!scrollTopBtn) return;
    if (window.scrollY > 450) scrollTopBtn.classList.add('show');
    else scrollTopBtn.classList.remove('show');
}

window.addEventListener('scroll', toggleScrollTop);
toggleScrollTop();

if (scrollTopBtn) {
    scrollTopBtn.addEventListener('click', () => {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });
}


// 7) Gallery Lightbox
if (window.GLightbox) {
    GLightbox({
        selector: '.glightbox',
        touchNavigation: true,
        loop: true,
        zoomable: true
    });
}

    // 5) Smooth Scrolling for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                window.scrollTo({
                    top: target.offsetTop - 80,
                    behavior: 'smooth'
                });
            }
        });
    });

    // 8) Dropdown Animation Enhancement
    document.querySelectorAll('.dropdown-toggle').forEach(toggle => {
        toggle.addEventListener('shown.bs.dropdown', function () {
            const menu = this.nextElementSibling;
            if (menu && menu.classList.contains('dropdown-menu')) {
                menu.classList.add('show');
            }
        });
        toggle.addEventListener('hidden.bs.dropdown', function () {
            const menu = this.nextElementSibling;
            if (menu && menu.classList.contains('dropdown-menu')) {
                menu.classList.remove('show');
            }
        });
    });
});

// MENU dropdown anchor -> open correct tab then scroll
const tabMap = {
  '#menu-breakfast': '#tab-breakfast',
  '#menu-lunch': '#tab-lunch',
  '#menu-dinner': '#tab-dinner',
  '#menu-dessert': '#tab-dessert'
};

document.querySelectorAll('a[href^="#menu-"]').forEach(link => {
  link.addEventListener('click', function (e) {
    const hash = this.getAttribute('href');
    const tabTarget = tabMap[hash];
    if (!tabTarget) return;

    e.preventDefault();

    // open tab
    const tabBtn = document.querySelector(`[data-bs-target="${tabTarget}"]`);
    if (tabBtn) {
      bootstrap.Tab.getOrCreateInstance(tabBtn).show();
    }

    // scroll after tab becomes visible
    setTimeout(() => {
      const el = document.querySelector(hash);
      if (el) {
        window.scrollTo({ top: el.offsetTop - 90, behavior: 'smooth' });
      }
    }, 120);
  });
});
