"use strict";

const mobileMenuToggle = document.querySelector("#mobileMenuToggle");
const mobileMenu = document.querySelector("#mobileMenu");
const siteHeader = document.querySelector(".site-header");
let wishlistReady = false;

document.querySelectorAll('a[href="#"]').forEach((link) => {
  link.classList.add("is-coming-soon");
  link.setAttribute("aria-disabled", "true");
  link.tabIndex = -1;
  link.title ||= "Bu özellik yakında";
  link.addEventListener("click", (event) => event.preventDefault());
});

function readStoredValue(key, fallback = null) {
  try {
    return window.localStorage.getItem(key) ?? fallback;
  } catch (error) {
    return fallback;
  }
}

function writeStoredValue(key, value) {
  try {
    window.localStorage.setItem(key, value);
    return true;
  } catch (error) {
    return false;
  }
}

const carouselButtonsByScroller = new Map();

document
  .querySelectorAll("[data-catalog-scroll][aria-controls]")
  .forEach((button) => {
    const scroller = document.getElementById(button.getAttribute("aria-controls"));
    if (!scroller) return;

    const buttons = carouselButtonsByScroller.get(scroller) || [];
    buttons.push(button);
    carouselButtonsByScroller.set(scroller, buttons);
  });

carouselButtonsByScroller.forEach((buttons, scroller) => {
  const updateScrollButtons = () => {
    const maxScrollLeft = Math.max(0, scroller.scrollWidth - scroller.clientWidth);
    const isAtStart = scroller.scrollLeft <= 1;
    const isAtEnd = scroller.scrollLeft >= maxScrollLeft - 1;

    buttons.forEach((button) => {
      const direction = Number(button.dataset.catalogScroll);
      button.disabled = direction < 0 ? isAtStart : isAtEnd;
    });
  };

  buttons.forEach((button) => {
    button.addEventListener("click", (event) => {
      event.preventDefault();
      event.stopPropagation();

      const direction = Number(button.dataset.catalogScroll);
      const scrollDistance = Math.max(scroller.clientWidth * 0.8, 260);

      scroller.scrollBy({
        left: direction * scrollDistance,
        behavior: "smooth",
      });
    });
  });

  scroller.addEventListener("scroll", updateScrollButtons, { passive: true });
  window.addEventListener("resize", updateScrollButtons, { passive: true });
  updateScrollButtons();
});

const storeSearch = document.querySelector("#storeSearch");
const catalogCards = Array.from(document.querySelectorAll(".catalog-game-card"));
const storeSearchEmpty = document.querySelector("#storeSearchEmpty");

if (storeSearch) {
  storeSearch.addEventListener("input", () => {
    const query = storeSearch.value.trim().toLocaleLowerCase("tr-TR");
    let visibleCount = 0;

    catalogCards.forEach((card) => {
      const title = card.querySelector("h3")?.textContent || "";
      const isVisible = title.toLocaleLowerCase("tr-TR").includes(query);
      card.hidden = !isVisible;
      if (isVisible) visibleCount += 1;
    });

    if (storeSearchEmpty) {
      storeSearchEmpty.hidden = query.length === 0 || visibleCount > 0;
    }

    window.dispatchEvent(new Event("resize"));
  });
}

document.querySelector(".search button")?.addEventListener("click", () => {
  storeSearch?.focus();
});

if (siteHeader) {
  let previousScrollY = window.scrollY;

  window.addEventListener("mousemove", (event) => {
    if (event.clientY <= 8 && siteHeader.classList.contains("is-hidden")) {
      siteHeader.classList.remove("is-hidden");
    }
  });

  window.addEventListener("scroll", () => {
    const currentScrollY = window.scrollY;
    const scrollDifference = currentScrollY - previousScrollY;

    if (currentScrollY <= 24 || scrollDifference < -6) {
      siteHeader.classList.remove("is-hidden");
    } else if (scrollDifference > 6) {
      siteHeader.classList.add("is-hidden");
    }

    previousScrollY = currentScrollY;
  }, { passive: true });
}

if (mobileMenuToggle && mobileMenu) {
  mobileMenuToggle.addEventListener("click", () => {
    const isOpen = mobileMenu.classList.toggle("show");
    mobileMenuToggle.setAttribute("aria-expanded", String(isOpen));
  });
  document.addEventListener("click", (event) => {
    if (
      !mobileMenu.contains(event.target) &&
      !mobileMenuToggle.contains(event.target)
    ) {
      mobileMenu.classList.remove("show");
      mobileMenuToggle.setAttribute("aria-expanded", "false");
    }
  });
  document.addEventListener("keydown", (event) => {
    if (event.key === "Escape") {
      mobileMenu.classList.remove("show");
      mobileMenuToggle.setAttribute("aria-expanded", "false");
    }
  });
}

const navMiddle = document.querySelector(".navbar-middle");
const activePill = document.querySelector(".nav-active-pill");

if (navMiddle && activePill) {
  const navLinks = navMiddle.querySelectorAll("a");
  const activeLink = navMiddle.querySelector("a.active");
  function moveActivePill(link) {
    if (!link) return;
    activePill.style.left = `${link.offsetLeft}px`;
    activePill.style.top = `${link.offsetTop}px`;
    activePill.style.width = `${link.offsetWidth}px`;
    activePill.style.height = `${link.offsetHeight}px`;
  }
  if (activeLink) {
    activePill.style.transition = "none";
    moveActivePill(activeLink);
    requestAnimationFrame(() => {
      requestAnimationFrame(() => {
        activePill.style.transition =
          "all 0.35s cubic-bezier(0.2, 0.8, 0.2, 1)";
      });
    });
  }
  navLinks.forEach((link) => {
    link.addEventListener("click", (event) => {
      const href = link.getAttribute("href");
      if (!href || href === "#") return;
      if (link.classList.contains("active")) return;
      event.preventDefault();
      navLinks.forEach((navLink) => {
        navLink.classList.remove("active");
      });
      link.classList.add("active");
      moveActivePill(link);
      setTimeout(() => {
        window.location.href = link.href;
      }, 350);
    });
  });
  window.addEventListener("resize", () => {
    const currentActiveLink = navMiddle.querySelector("a.active");
    if (currentActiveLink) moveActivePill(currentActiveLink);
  });
}

const featuredOptions = Array.from(
  document.querySelectorAll(".featured-game-list .featured-game-option"),
);
const featuredGames = featuredOptions.map((option) => ({
  id: Number(option.dataset.featuredId),
  title: option.dataset.featuredTitle || "",
  description: option.dataset.featuredDescription || "",
  image: option.dataset.featuredImage || "",
}));

const featuredBanner = document.querySelector("#featuredBanner");
const bannerSlides = featuredGames;

bannerSlides.forEach((game) => {
  if (!game.image) return;

  const img = new Image();
  img.src = game.image;
});

let currentGameIndex = 0;
const bannerTitle = document.querySelector("#bannerTitle");
const bannerDescription = document.querySelector("#bannerDescription");
const detailsButton = document.querySelector("#detailsButton");
const bannerWishlistButton = document.querySelector("#bannerWishlistButton");
const bannerProgressFill = document.querySelector(".banner-progress-fill");
let bannerInterval;
let bannerTransitionTimeout;

function showFeaturedGame() {
  if (!featuredBanner || !bannerTitle || !bannerDescription) {
    return;
  }

  const game = bannerSlides[currentGameIndex];

  if (!game) return;

  featuredOptions.forEach((option, index) => {
    const isActive = index === currentGameIndex;
    option.classList.toggle("active", isActive);
    option.setAttribute("aria-pressed", String(isActive));
  });

  const detailsUrl = new URL(featuredBanner.dataset.detailsUrl, window.location.origin);
  detailsUrl.searchParams.set("id", String(game.id));

  if (detailsButton) {
    detailsButton.textContent = "İncele";
    detailsButton.href = detailsUrl.pathname + detailsUrl.search;
  }
  if (bannerWishlistButton) {
    bannerWishlistButton.dataset.gameId = String(game.id);
    bannerWishlistButton.dataset.gameTitle = game.title;
    bannerWishlistButton.dataset.gameImage = game.image;
    bannerWishlistButton.dataset.gameUrl = detailsUrl.pathname + detailsUrl.search;
  }
  if (wishlistReady) {
    refreshWishlistButtons();
  }
  clearTimeout(bannerTransitionTimeout);
  featuredBanner.classList.add("banner-entering");

  bannerTransitionTimeout = setTimeout(() => {
    bannerTitle.textContent = game.title;
    bannerDescription.textContent = game.description;

    if (game.image) {
      featuredBanner.style.setProperty(
        "--banner-image",
        `linear-gradient(to right, rgba(0, 0, 0, 0.75) 0%, rgba(0, 0, 0, 0.35) 50%, rgba(0, 0, 0, 0) 100%), url("${game.image}")`,
      );
    }

    requestAnimationFrame(() => {
      featuredBanner.classList.remove("banner-entering");
    });
  }, 250);
}

function advanceFeaturedGame() {
  if (bannerSlides.length < 2) return;

  currentGameIndex = (currentGameIndex + 1) % bannerSlides.length;
  showFeaturedGame();
  resetBannerProgress();
}

function restartBannerInterval() {
  clearInterval(bannerInterval);

  if (bannerSlides.length > 1 && featuredBanner) {
    bannerInterval = setInterval(advanceFeaturedGame, 5000);
  }
}

function resetBannerProgress() {
  if (!bannerProgressFill) return;

  bannerProgressFill.style.transition = "none";
  bannerProgressFill.style.width = "0%";

  requestAnimationFrame(() => {
    requestAnimationFrame(() => {
      bannerProgressFill.style.transition = "width 5s linear";
      bannerProgressFill.style.width = "100%";
    });
  });
}

// Banner otomatik geçişi
if (bannerSlides.length > 0 && featuredBanner) {
  showFeaturedGame();
  resetBannerProgress();
  restartBannerInterval();

  featuredOptions.forEach((option, index) => {
    option.addEventListener("click", () => {
      currentGameIndex = index;
      showFeaturedGame();
      resetBannerProgress();
      restartBannerInterval();
    });
  });
}

// Profil menüsü
const profileMenuToggle = document.querySelector("#profileMenuToggle");
const profileDropdown = document.querySelector("#profileDropdown");

if (profileMenuToggle && profileDropdown) {
  profileMenuToggle.addEventListener("click", () => {
    const isOpen = profileDropdown.classList.toggle("show");

    profileMenuToggle.classList.toggle("open", isOpen);
    profileMenuToggle.setAttribute("aria-expanded", String(isOpen));
  });

  document.addEventListener("click", (event) => {
    if (
      !profileDropdown.contains(event.target) &&
      !profileMenuToggle.contains(event.target)
    ) {
      profileDropdown.classList.remove("show");
      profileMenuToggle.classList.remove("open");
      profileMenuToggle.setAttribute("aria-expanded", "false");
    }
  });

  document.addEventListener("keydown", (event) => {
    if (event.key === "Escape") {
      profileDropdown.classList.remove("show");
      profileMenuToggle.classList.remove("open");
      profileMenuToggle.setAttribute("aria-expanded", "false");
    }
  });
}

// Tema değiştirme
const darkThemeBtn = document.querySelector(".dark-theme-btn");
const lightThemeBtn = document.querySelector(".light-theme-btn");

function setTheme(theme) {
  if (theme !== "dark" && theme !== "light") {
    theme = "light";
  }

  document.body.classList.remove("dark-mode", "light-mode");
  document.body.classList.add(theme + "-mode");

  writeStoredValue("rune-theme", theme);
}

setTheme(readStoredValue("rune-theme", "light"));

darkThemeBtn?.addEventListener("click", () => setTheme("dark"));
lightThemeBtn?.addEventListener("click", () => setTheme("light"));

function trapDialogFocus(dialog, event) {
  if (event.key !== "Tab") return;

  const focusable = Array.from(
    dialog.querySelectorAll(
      'a[href], button:not([disabled]), input:not([disabled]), [tabindex]:not([tabindex="-1"])',
    ),
  );

  if (focusable.length === 0) {
    event.preventDefault();
    dialog.focus();
    return;
  }

  const first = focusable[0];
  const last = focusable[focusable.length - 1];

  if (!dialog.contains(document.activeElement)) {
    event.preventDefault();
    first.focus();
  } else if (event.shiftKey && document.activeElement === first) {
    event.preventDefault();
    last.focus();
  } else if (!event.shiftKey && document.activeElement === last) {
    event.preventDefault();
    first.focus();
  }
}

// İstek listesi bu tarayıcıda saklanır; hesap senkronizasyonu henüz yoktur.
const wishlistKey = "rune-wishlist-v1";
const wishlistOverlay = document.querySelector("#wishlistOverlay");
const wishlistDialog = wishlistOverlay?.querySelector('[role="dialog"]');
const wishlistClose = document.querySelector("#wishlistClose");
const wishlistItems = document.querySelector("#wishlistItems");
const wishlistEmpty = document.querySelector("#wishlistEmpty");
const wishlistOpenTriggers = document.querySelectorAll("[data-wishlist-open]");
let wishlist = [];
let wishlistLastFocus = null;

function getLocalPath(value) {
  if (typeof value !== "string" || value.length === 0) return null;

  try {
    const url = new URL(value, window.location.origin);
    if (url.origin !== window.location.origin) return null;
    return `${url.pathname}${url.search}${url.hash}`;
  } catch (error) {
    return null;
  }
}

function isSafeGameDetailsUrl(value) {
  const localPath = getLocalPath(value);
  return localPath !== null && /^\/Games\/Details(?:\/|$)/i.test(localPath);
}

document.querySelectorAll(".game-media-gallery").forEach((gallery) => {
  const mainImage = gallery.querySelector("[data-gallery-main-image]");
  const thumbnails = gallery.querySelectorAll("[data-gallery-select]");

  thumbnails.forEach((thumbnail) => {
    thumbnail.addEventListener("click", () => {
      const imagePath = getLocalPath(thumbnail.dataset.galleryImage);
      if (!mainImage || !imagePath) return;

      mainImage.src = imagePath;
      mainImage.alt = thumbnail.dataset.galleryAlt || "Oyun görseli";

      thumbnails.forEach((item) => {
        const isSelected = item === thumbnail;
        item.classList.toggle("active", isSelected);
        item.setAttribute("aria-pressed", String(isSelected));
      });
    });
  });
});

document.querySelectorAll(".game-detail-tabs a").forEach((tab) => {
  tab.addEventListener("click", () => {
    document.querySelectorAll(".game-detail-tabs a").forEach((item) => {
      item.classList.toggle("active", item === tab);
    });
  });
});

try {
  const storedWishlist = JSON.parse(readStoredValue(wishlistKey, "[]"));
  wishlist = Array.isArray(storedWishlist)
    ? storedWishlist.filter(
        (item) =>
          item &&
          item.id !== undefined &&
          item.title &&
          isSafeGameDetailsUrl(item.url),
      )
    : [];
} catch (error) {
  wishlist = [];
}

function renderWishlist() {
  if (!wishlistItems || !wishlistEmpty) return;

  wishlistItems.replaceChildren();
  wishlistEmpty.hidden = wishlist.length > 0;

  wishlist.forEach((item) => {
    const row = document.createElement("li");
    row.className = "wishlist-item";

    const imagePath = getLocalPath(item.image);
    if (imagePath) {
      const image = document.createElement("img");
      image.src = imagePath;
      image.alt = "";
      image.loading = "lazy";
      row.append(image);
    }

    const link = document.createElement("a");
    link.href = getLocalPath(item.url);
    link.textContent = item.title;
    row.append(link);

    const removeButton = document.createElement("button");
    removeButton.type = "button";
    removeButton.dataset.wishlistRemove = String(item.id);
    removeButton.setAttribute("aria-label", item.title + " oyununu istek listesinden kaldır");
    removeButton.textContent = "Kaldır";
    row.append(removeButton);

    wishlistItems.append(row);
  });
}

function refreshWishlistButtons() {
  document.querySelectorAll("[data-wishlist-toggle]").forEach((button) => {
    const isSaved = wishlist.some((item) => String(item.id) === button.dataset.gameId);
    button.setAttribute("aria-pressed", String(isSaved));
    button.classList.toggle("is-wishlisted", isSaved);
    button.title = isSaved ? "İstek listesinden kaldır" : "İstek listesine ekle";
    const gameTitle = button.dataset.gameTitle || "Oyun";
    const wishlistLabel = button.querySelector("[data-wishlist-label]");
    if (wishlistLabel) {
      wishlistLabel.textContent = isSaved
        ? "İstek Listesinden Kaldır"
        : "İstek Listesine Ekle";
    }
    button.setAttribute(
      "aria-label",
      `${isSaved ? "İstek listesinden kaldır" : "İstek listesine ekle"}: ${gameTitle}`,
    );
  });

  renderWishlist();
}

function openWishlist() {
  if (!wishlistOverlay || !wishlistDialog) return;
  wishlistLastFocus = document.activeElement;
  wishlistOverlay.hidden = false;
  document.body.classList.add("wishlist-open");
  refreshWishlistButtons();
  requestAnimationFrame(() => wishlistClose?.focus({ preventScroll: true }));
}

function closeWishlist() {
  if (!wishlistOverlay) return;
  wishlistOverlay.hidden = true;
  document.body.classList.remove("wishlist-open");
  wishlistLastFocus?.focus?.({ preventScroll: true });
}

wishlistOpenTriggers.forEach((trigger) => {
  trigger.addEventListener("click", (event) => {
    event.preventDefault();
    if (mobileMenu?.contains(trigger)) {
      mobileMenu.classList.remove("show");
      mobileMenuToggle?.setAttribute("aria-expanded", "false");
      mobileMenuToggle?.focus();
    }
    openWishlist();
  });
});

wishlistClose?.addEventListener("click", closeWishlist);

document.addEventListener("click", (event) => {
  const target = event.target instanceof Element ? event.target : null;
  if (!target) return;

  const removeButton = target.closest("[data-wishlist-remove]");
  if (removeButton) {
    const id = removeButton.dataset.wishlistRemove;
    wishlist = wishlist.filter((item) => String(item.id) !== id);
    writeStoredValue(wishlistKey, JSON.stringify(wishlist));
    refreshWishlistButtons();
    return;
  }

  const toggleButton = target.closest("[data-wishlist-toggle]");
  if (!toggleButton || !toggleButton.dataset.gameId) return;

  const id = toggleButton.dataset.gameId;
  const isSaved = wishlist.some((item) => String(item.id) === id);
  if (isSaved) {
    wishlist = wishlist.filter((item) => String(item.id) !== id);
  } else {
    const gameUrl = toggleButton.dataset.gameUrl || "";
    if (!isSafeGameDetailsUrl(gameUrl)) return;

    wishlist.push({
      id,
      title: toggleButton.dataset.gameTitle || "Oyun",
      image: toggleButton.dataset.gameImage || "",
      url: getLocalPath(gameUrl),
    });
  }

  writeStoredValue(wishlistKey, JSON.stringify(wishlist));
  refreshWishlistButtons();
});

refreshWishlistButtons();
wishlistReady = true;

const whatsNewOverlay = document.querySelector("#whatsNewOverlay");
const whatsNewDialog = document.querySelector("#whatsNewDialog");
const whatsNewClose = document.querySelector("#whatsNewClose");
const whatsNewOpenTriggers = document.querySelectorAll("[data-whats-new-open]");
let closeWhatsNewDialog = () => {};

if (whatsNewOverlay && whatsNewDialog && whatsNewClose) {
  const releaseVersion = whatsNewDialog.dataset.releaseVersion;
  const seenVersionKey = "rune-whats-new-seen-version";
  const seenVersion = readStoredValue(seenVersionKey);
  let whatsNewLastFocus = null;

  closeWhatsNewDialog = () => {
    whatsNewOverlay.hidden = true;
    document.body.classList.remove("whats-new-open");
    writeStoredValue(seenVersionKey, releaseVersion);
    whatsNewLastFocus?.focus?.({ preventScroll: true });
  };

  const openWhatsNewDialog = () => {
    whatsNewLastFocus = document.activeElement;
    whatsNewOverlay.hidden = false;
    document.body.classList.add("whats-new-open");
    requestAnimationFrame(() => whatsNewClose.focus({ preventScroll: true }));
  };

  whatsNewClose.addEventListener("click", closeWhatsNewDialog);

  whatsNewOpenTriggers.forEach((trigger) => {
    trigger.addEventListener("click", (event) => {
      event.preventDefault();

      if (mobileMenu?.contains(trigger)) {
        mobileMenu.classList.remove("show");
        mobileMenuToggle?.setAttribute("aria-expanded", "false");
        mobileMenuToggle?.focus();
      }

      openWhatsNewDialog();
    });
  });

  if (seenVersion !== releaseVersion) {
    openWhatsNewDialog();
  }
}

document.addEventListener("keydown", (event) => {
  if (event.key === "Escape" && whatsNewOverlay && !whatsNewOverlay.hidden) {
    closeWhatsNewDialog();
  } else if (whatsNewDialog && whatsNewOverlay && !whatsNewOverlay.hidden) {
    trapDialogFocus(whatsNewDialog, event);
  }

  if (event.key === "Escape" && wishlistOverlay && !wishlistOverlay.hidden) {
    closeWishlist();
  } else if (wishlistDialog && wishlistOverlay && !wishlistOverlay.hidden) {
    trapDialogFocus(wishlistDialog, event);
  }
});
