// ===============================
// Navbar móvil
// ===============================
document.addEventListener("DOMContentLoaded", function () {
    const menuToggle = document.getElementById("mobile-menu");
    const navLinks = document.querySelector(".nav-links");

    if (menuToggle && navLinks) {
        menuToggle.addEventListener("click", () => {
            navLinks.classList.toggle("active");
        });
    }
});

// ===============================
// Scroll suave entre vistas y secciones
// ===============================
document.addEventListener("DOMContentLoaded", function () {

    // 1️⃣ Si entrás con un hash (/#projects, /#about, etc.)
    if (window.location.hash) {
        setTimeout(() => {
            const target = document.querySelector(window.location.hash);
            if (target) target.scrollIntoView({ behavior: "smooth" });
        }, 300);
    }

    // 2️⃣ Manejo de clics en enlaces con #
    document.querySelectorAll('a[href*="#"]').forEach(link => {
        link.addEventListener("click", function (e) {

            // 🚨 EVITAR ROMPER MODALES BOOTSTRAP
            if (this.hasAttribute("data-bs-toggle")) return;

            const href = this.getAttribute("href");
            const [path, hash] = href.split("#");

            // Si no hay hash → dejo navegar normal
            if (!hash) return;

            const isHome =
                window.location.pathname.toLowerCase().includes("/home/index") ||
                window.location.pathname === "/" ||
                window.location.pathname === "";

            if (!isHome && path && !path.endsWith("Index")) {
                return; // Permite cargar /Home/Index#about
            }

            // 🚀 Scroll suave en la página Home
            e.preventDefault();
            const target = document.getElementById(hash);
            if (target) {
                target.scrollIntoView({ behavior: "smooth" });
                history.pushState(null, "", `#${hash}`);
            }
        });
    });
});

// ===============================
// Animación de entrada (scroll reveal)
// ===============================
document.addEventListener("scroll", () => {
    document.querySelectorAll(".reveal").forEach(el => {
        const rect = el.getBoundingClientRect();
        if (rect.top < window.innerHeight - 100) {
            el.classList.add("visible");
        }
    });
});
