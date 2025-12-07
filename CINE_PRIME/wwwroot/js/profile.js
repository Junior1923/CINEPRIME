// Tabs
document.querySelectorAll('.tab-btn').forEach(btn => {
    btn.addEventListener('click', () => {
        document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
        btn.classList.add('active');
        const tab = btn.dataset.tab;
        document.querySelectorAll('.tab-content').forEach(t => t.classList.remove('active'));
        document.getElementById(tab).classList.add('active');
    });
});

// Preview avatar
const fileInput = document.getElementById('profilePicture');
const avatarPreview = document.getElementById('avatarPreview');
const originalAvatar = avatarPreview ? avatarPreview.src : '';

if (fileInput) {
    fileInput.addEventListener('change', e => {
        const file = e.target.files[0];
        if (!file) return;
        if (!file.type.startsWith('image/')) return;
        const reader = new FileReader();
        reader.onload = function (ev) {
            avatarPreview.src = ev.target.result;
        };
        reader.readAsDataURL(file);
    });
}

// Reset avatar preview (does not delete server image, only preview)
const resetBtn = document.getElementById('resetAvatarBtn');
if (resetBtn) {
    resetBtn.addEventListener('click', () => {
        avatarPreview.src = originalAvatar || '/img/default-avatar.png';
        // optionally: you could send a request to server to delete avatar (not implemented here)
    });
}