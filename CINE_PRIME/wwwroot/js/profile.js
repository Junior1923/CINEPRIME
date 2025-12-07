document.addEventListener("DOMContentLoaded", () => {

    /* ========================
       TABS
       ======================== */
    const tabs = document.querySelectorAll(".pv-tab");
    const panes = document.querySelectorAll(".pv-tab-pane");

    tabs.forEach((tab) => {
        tab.addEventListener("click", () => {
            tabs.forEach(t => t.classList.remove("active"));
            panes.forEach(p => p.classList.remove("active"));

            tab.classList.add("active");
            document.querySelector(tab.dataset.target).classList.add("active");
        });
    });

    /* ========================
       AVATAR PREVIEW
       ======================== */
    const fileInput = document.getElementById("fileInput");
    const preview = document.getElementById("avatarPreview");
    const fileError = document.getElementById("fileError");
    const reset = document.getElementById("resetAvatarBtnSimple");

    const originalSrc = preview.src;

    fileInput?.addEventListener("change", (e) => {
        const file = e.target.files[0];
        if (!file) return;

        if (!["image/png", "image/jpeg", "image/jpg"].includes(file.type)) {
            fileError.textContent = "Formato inválido. Usa PNG/JPG.";
            fileInput.value = "";
            return;
        }
        if (file.size > 2 * 1024 * 1024) {
            fileError.textContent = "El archivo supera los 2MB.";
            fileInput.value = "";
            return;
        }
        fileError.textContent = "";

        const reader = new FileReader();
        reader.onload = (ev) => preview.src = ev.target.result;
        reader.readAsDataURL(file);
    });

    reset?.addEventListener("click", () => {
        preview.src = originalSrc;
        fileInput.value = "";
    });

    /* ========================
       TOASTS (TempData)
       ======================== */
    const toastRoot = document.getElementById("profileToasts");

    function showToast(msg) {
        if (!msg) return;
        const div = document.createElement("div");
        div.className = "pv-toast-msg";
        div.textContent = msg;
        toastRoot.appendChild(div);

        setTimeout(() => div.style.opacity = "0", 3500);
        setTimeout(() => div.remove(), 4200);
    }

    showToast(_pvMsgs.updated);
    showToast(_pvMsgs.noChanges);
});

document.querySelectorAll(".prime-tab").forEach(btn => {
    btn.addEventListener("click", () => {

        document.querySelector(".prime-tab.active")?.classList.remove("active");
        btn.classList.add("active");

        const tab = btn.dataset.tab;

        document.querySelector(".prime-content.active")?.classList.remove("active");
        document.querySelector("#tab-" + tab).classList.add("active");
    });
});
