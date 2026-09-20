CREATE TABLE IF NOT EXISTS products (
id INTEGER PRIMARY KEY,
code TEXT NOT NULL UNIQUE,
reference TEXT UNIQUE NOT NULL,
description TEXT NOT NULL,
stock REAL DEFAULT 0 CHECK(stock >= 0),
markup REAL NOT NULL CHECK(markup > 0),
cost REAL DEFAULT 0 CHECK(cost >= 0),
price REAL DEFAULT 0 CHECK(price >= 0),
unit_measurement TEXT default 'unidad' CHECK(unit_measurement in ('unidad', 'caja', 'botella', 'frasco', 'lata', 'paquete')),
created_at TEXT NOT NULL DEFAULT (DATE('now'))
);


CREATE TABLE IF NOT EXISTS suppliers (
id INTEGER PRIMARY KEY,
name TEXT NOT NULL,
address TEXT NOT NULL,
phone_number TEXT NOT NULL,
EMAIL TEXT
);

CREATE TABLE IF NOT EXISTS purchases(
id INTEGER PRIMARY KEY,
invoice_number TEXT NOT NULL, 
created_at TEXT DEFAULT (DATE('now')),
supplier_id INTEGER NOT NULL,
FOREIGN KEY (supplier_id) REFERENCES suppliers(id)
);

CREATE TABLE IF NOT EXISTS purchase_details (
id INTEGER PRIMARY KEY,
purchase_id INTEGER NOT NULL,
product_id INTEGER NOT NULL,
quantity REAL NOT NULL CHECK(quantity > 0),
unit_cost REAL NOT NULL CHECK(unit_cost > 0),
tax_included  INTEGER DEFAULT 0 CHECK(tax_included in (1,0)),
tax_rate REAL DEFAULT 18.0 CHECK(tax_rate >= 0),
FOREIGN KEY (purchase_id) REFERENCES purchases(id) ON DELETE CASCADE,
FOREIGN KEY (product_id) REFERENCES products(id)
);

CREATE TABLE IF NOT EXISTS client (
id INTEGER NOT NULL PRIMARY KEY,
name TEXT NOT NULL,
tax_id TEXT UNIQUE,
address TEXT,
city TEXT,
phone_number TEXT,
EMAIL TEXT,
credit_limit REAL DEFAULT 0 CHECK(credit_limit >= 0),
is_active INTEGER DEFAULT 1 CHECK(is_active in (0,1)),
created_at TEXT NOT NULL DEFAULT (DATE('now'))
);

CREATE TABLE IF NOT EXISTS invoices (
id INTEGER NOT NULL PRIMARY KEY,
client_id INTEGER,
client_name TEXT,
client_tax_id TEXT,
invoice_number TEXT UNIQUE,
payment_type TEXT DEFAULT 'cash' CHECK(payment_type IN ('cash', 'credit', 'card', 'transfer')),
total_amount REAL DEFAULT 0 CHECK(total_amount >= 0),
tax_amount REAL DEFAULT 0 CHECK(tax_amount >= 0),
discount_amount REAL DEFAULT 0 CHECK(discount_amount >= 0),
created_at TEXT NOT NULL DEFAULT (DATE('now')),
FOREIGN KEY (client_id) REFERENCES client(id)
);

--invoice details
CREATE TABLE IF NOT EXISTS invoice_details (
id INTEGER PRIMARY KEY,
invoice_id INTEGER NOT NULL,
product_id INTEGER NOT NULL,
quantity REAL NOT NULL CHECK(quantity > 0),
unit_price REAL NOT NULL CHECK(unit_price > 0),
tax_rate REAL DEFAULT 18.0 CHECK(tax_rate >= 0),
subtotal REAL NOT NULL CHECK(subtotal >=0),

FOREIGN KEY (invoice_id) REFERENCES invoices(id) ON DELETE CASCADE,
FOREIGN KEY (product_id) REFERENCES products(id)
);
