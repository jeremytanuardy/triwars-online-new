# Setup Triwars Online di Windows

## ✅ DONE di Mac
- Git repo sudah dibuat
- Git LFS sudah configured
- Code terbaru sudah di-push ke GitHub: `triwars-online-new`

---

## 🪟 Di Windows - Step by Step

### 1. Install Git dan Git LFS

**Download & Install Git:**
```
https://git-scm.com/download/win
```
- Install dengan default settings
- Pastikan centang "Git LFS" saat install

**Verify Git LFS:**
Buka **Command Prompt** atau **PowerShell**:
```powershell
git --version
git lfs version
```
Harusnya keluar version number.

---

### 2. Clone Repository

**PENTING:** Pilih lokasi dengan cukup space (minimal 20GB free space)

```powershell
# Masuk ke folder dimana lu mau taruh project
# Contoh: D:\GameDev\
cd D:\GameDev

# Clone repo (ini akan download SEMUA files)
git clone https://github.com/jeremytanuardy/triwars-online-new.git

# Masuk ke folder project
cd triwars-online-new
```

**CATATAN:** Clone pertama kali akan lama (14GB), tergantung internet lu.
Git LFS akan download semua file besar (models, textures, audio).

---

### 3. Buka di Unity Hub

1. Buka **Unity Hub**
2. Klik **"Add"** → **"Add project from disk"**
3. Pilih folder: `D:\GameDev\triwars-online-new` (atau dimana lu clone)
4. Unity Hub akan detect Unity version yang dibutuhkan
5. Jika Unity version belum installed, install dulu (Unity 2022.3.x LTS)
6. Klik project untuk buka

**First Open:** Unity akan import semua assets (~5-10 menit pertama kali).

---

### 4. Workflow Mac ↔ Windows

#### **Di Windows (setelah kerja):**
```powershell
# Simpan changes
git add .
git commit -m "Fix dari Windows: [describe what you did]"
git push origin main
```

#### **Di Mac (untuk continue dari Windows):**
```bash
cd "/Users/jeremytanuardy/Dev Game/Triwars Online/Triwars Online New"

# Download changes dari Windows
git pull origin main
```

#### **Di Windows (untuk continue dari Mac):**
```powershell
cd D:\GameDev\triwars-online-new

# Download changes dari Mac
git pull origin main
```

---

## ⚠️ ATURAN PENTING

### WAJIB sebelum switch device:

1. **SAVE scene di Unity** (Ctrl+S / Cmd+S)
2. **CLOSE Unity Editor** (jangan tinggalin Unity kebuka)
3. **Commit & Push:**
   ```bash
   git add .
   git commit -m "Work session done - switching to [Mac/Windows]"
   git push origin main
   ```

### WAJIB sebelum buka Unity di device lain:

1. **Pull latest changes:**
   ```bash
   git pull origin main
   ```
2. **Tunggu sampai selesai pull**
3. **Baru buka Unity**

---

## 🚨 Troubleshooting

### "Library/" folder missing saat clone
**NORMAL!** `Library/` adalah auto-generated, Unity akan rebuild saat pertama buka.

### Git LFS files tidak ke-download (file kecil semua)
```powershell
git lfs pull
```

### Conflict saat push/pull
```powershell
# Lihat file yang conflict
git status

# Jika conflict di .unity scene file - ambil salah satu:
# Pilih versi dari server (Mac):
git checkout --theirs Assets/Scenes/SampleScene.unity
git add Assets/Scenes/SampleScene.unity

# ATAU pilih versi local (Windows):
git checkout --ours Assets/Scenes/SampleScene.unity
git add Assets/Scenes/SampleScene.unity

# Lalu commit
git commit -m "Resolved conflict - kept [Mac/Windows] version"
git push origin main
```

### Project size terlalu gede (>14GB)
**CARA KURANGI SIZE:**
1. Hapus folder `Assets/ImportedAssets/` - purchased assets bisa re-download
2. Hapus folder `Assets/Hovl Studio/` - VFX belum dipakai
3. Hapus folder `Assets/Mirror/Examples/` - contoh Mirror ga perlu

Update `.gitignore` untuk exclude mereka.

---

## 📝 Tips

- **Jangan lupa commit tiap kali selesai work session**
- **Commit message yang jelas**: `"Fixed attack bug"` bukan `"changes"`
- **Pull dulu sebelum push** untuk avoid conflict
- **Backup manual** sesekali (copy folder ke external HDD)

---

## 🆘 Emergency Contacts

- GitHub repo: https://github.com/jeremytanuardy/triwars-online-new
- Jika ada masalah, cek GitHub issues atau contact Jeremy

---

## Current Status

**Last sync:** December 4, 2025
**Latest commit:** Diagnostic logging for body penetration debugging
**Branch:** main
**Unity Version:** 2022.3.x LTS
**Total size:** ~14GB (with imported assets)
