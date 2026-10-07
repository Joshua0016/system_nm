-- ============================================================
-- 1. SEGURIDAD Y USUARIOS (LOGIN)
-- ============================================================

CREATE TABLE IF NOT EXISTS users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username TEXT NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,                  -- Almacena el hash de la contraseña (bcrypt)
    full_name TEXT NOT NULL,
    role TEXT NOT NULL CHECK(role IN ('ADMIN', 'CASHIER', 'SUPERVISOR')),
    is_active INTEGER DEFAULT 1 CHECK(is_active IN (0,1)),
    created_at TEXT NOT NULL DEFAULT (DATETIME('now'))
);

-- ============================================================
-- 2. CATÁLOGOS BASE
-- ============================================================

CREATE TABLE IF NOT EXISTS products (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    code TEXT NOT NULL UNIQUE,
    reference TEXT UNIQUE NOT NULL,
    description TEXT NOT NULL,
    stock REAL DEFAULT 0 CHECK(stock >= 0),
    markup REAL NOT NULL CHECK(markup > 0),
    cost REAL DEFAULT 0 CHECK(cost >= 0),
    price REAL DEFAULT 0 CHECK(price >= 0),
    unit_measurement TEXT DEFAULT 'unidad' CHECK(unit_measurement IN ('unidad', 'caja', 'botella', 'frasco', 'lata', 'paquete')),
    created_at TEXT NOT NULL DEFAULT (DATETIME('now'))
);

CREATE TABLE IF NOT EXISTS suppliers (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    address TEXT NOT NULL,
    phone_number TEXT NOT NULL,
    email TEXT
);

CREATE TABLE IF NOT EXISTS client (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    tax_id TEXT UNIQUE,                           -- RNC o Cédula
    address TEXT,
    city TEXT,
    phone_number TEXT,
    email TEXT,
    credit_limit REAL DEFAULT 0 CHECK(credit_limit >= 0),
    is_active INTEGER DEFAULT 1 CHECK(is_active IN (0,1)),
    created_at TEXT NOT NULL DEFAULT (DATETIME('now'))
);

-- ============================================================
-- 3. COMPRAS / ENTRADAS DE MERCANCÍA
-- ============================================================

CREATE TABLE IF NOT EXISTS purchases (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    invoice_number TEXT NOT NULL, 
    supplier_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,                     -- Usuario/Comprador que registró la entrada
    created_at TEXT NOT NULL DEFAULT (DATETIME('now')),
    FOREIGN KEY (supplier_id) REFERENCES suppliers(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS purchase_details (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    purchase_id INTEGER NOT NULL,
    product_id INTEGER NOT NULL,
    quantity REAL NOT NULL CHECK(quantity > 0),
    unit_cost REAL NOT NULL CHECK(unit_cost > 0),
    tax_included INTEGER DEFAULT 0 CHECK(tax_included IN (1,0)),
    tax_rate REAL DEFAULT 0.18 CHECK(tax_rate >= 0), -- Representación decimal (0.18 = 18%)
    FOREIGN KEY (purchase_id) REFERENCES purchases(id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
);

-- ============================================================
-- 4. FACTURACIÓN Y VENTAS
-- ============================================================

CREATE TABLE IF NOT EXISTS invoices (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    client_id INTEGER,
    user_id INTEGER NOT NULL,                     -- Cajero/Usuario que emitió la factura
    client_name TEXT,
    client_tax_id TEXT,
    invoice_number TEXT UNIQUE,                   -- NCF de la Factura (Ej: B0100000001)
    payment_type TEXT DEFAULT 'cash' CHECK(payment_type IN ('cash', 'credit', 'card', 'transfer')),
    subtotal REAL DEFAULT 0 CHECK(subtotal >= 0),
    tax_amount REAL DEFAULT 0 CHECK(tax_amount >= 0),
    discount_amount REAL DEFAULT 0 CHECK(discount_amount >= 0),
    total_amount REAL DEFAULT 0 CHECK(total_amount >= 0),
    created_at TEXT NOT NULL DEFAULT (DATETIME('now')),
    FOREIGN KEY (client_id) REFERENCES client(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS invoice_details (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    invoice_id INTEGER NOT NULL,
    product_id INTEGER NOT NULL,
    quantity REAL NOT NULL CHECK(quantity > 0),
    unit_price REAL NOT NULL CHECK(unit_price > 0),
    tax_rate REAL DEFAULT 0.18 CHECK(tax_rate >= 0),
    subtotal REAL NOT NULL CHECK(subtotal >= 0),
    FOREIGN KEY (invoice_id) REFERENCES invoices(id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
);

-- ============================================================
-- 5. DEVOLUCIONES / NOTAS DE CRÉDITO (REQUISITO DGII)
-- ============================================================

CREATE TABLE IF NOT EXISTS returns (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    invoice_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,                     -- Usuario/Supervisor que autorizó la devolución
    ncf TEXT UNIQUE NOT NULL,                      -- NCF Nota de Crédito (Ej: B0400000001)
    affected_ncf TEXT NOT NULL,                    -- NCF de la factura original
    reason_code TEXT NOT NULL CHECK(reason_code IN ('01', '02', '03', '04')), -- Formato DGII
    subtotal REAL NOT NULL CHECK(subtotal >= 0),
    tax_amount REAL NOT NULL CHECK(tax_amount >= 0),
    total REAL NOT NULL CHECK(total >= 0),
    created_at TEXT NOT NULL DEFAULT (DATETIME('now')),
    FOREIGN KEY (invoice_id) REFERENCES invoices(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS return_details (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    return_id INTEGER NOT NULL,
    product_id INTEGER NOT NULL,
    quantity REAL NOT NULL CHECK(quantity > 0),
    unit_price REAL NOT NULL CHECK(unit_price > 0),
    tax_rate REAL DEFAULT 0.18 CHECK(tax_rate >= 0),
    subtotal REAL NOT NULL CHECK(subtotal >= 0),
    tax_amount REAL NOT NULL CHECK(tax_amount >= 0),
    FOREIGN KEY (return_id) REFERENCES returns(id),
    FOREIGN KEY (product_id) REFERENCES products(id)
);

-- ============================================================
-- 6. AUDITORÍA Y KÁRDEX DE INVENTARIO
-- ============================================================

CREATE TABLE IF NOT EXISTS inventory_movements (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    product_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,                     -- Quién ejecutó el movimiento de stock
    type TEXT NOT NULL CHECK(type IN ('IN','OUT')),
    concept TEXT NOT NULL CHECK(concept IN ('purchase', 'sale', 'adjustment_in', 'adjustment_out', 'return')),
    quantity REAL NOT NULL CHECK(quantity > 0),
    stock_before REAL NOT NULL CHECK(stock_before >= 0),
    stock_after REAL NOT NULL CHECK(stock_after >= 0),
    reference_invoice_id INTEGER,                 -- ID opcional de referencia (factura, compra o devolución)
    created_at TEXT NOT NULL DEFAULT (DATETIME('now')),
    FOREIGN KEY (product_id) REFERENCES products(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

-- ============================================================
-- 7. ÍNDICES DE RENDIMIENTO (B-TREE)
-- ============================================================

CREATE INDEX IF NOT EXISTS idx_movements_product_date ON inventory_movements(product_id, created_at);
CREATE INDEX IF NOT EXISTS idx_invoices_date ON invoices(created_at);
CREATE INDEX IF NOT EXISTS idx_invoices_user ON invoices(user_id);
CREATE INDEX IF NOT EXISTS idx_returns_date ON returns(created_at);


-- Insertar usuario 'admin' por defecto únicamente si no existe
INSERT OR IGNORE INTO users (id, username, password_hash, full_name, role)
VALUES (
    1, 
    'admin', 
    '$2b$10$wE9vN3/SgX8Y4IeZq.1234567890abcdef...', -- Hash de la contraseña 'admin123'
    'Administrador Inicial', 
    'ADMIN'
);