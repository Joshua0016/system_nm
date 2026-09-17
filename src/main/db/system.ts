import BetterSqlite3 from 'better-sqlite3';
import path from 'path';

import schemasql from './schema.sql?raw';

const dbPath = path.join(__dirname, '../../../system.db');

export const db : BetterSqlite3.Database = new BetterSqlite3(dbPath)

db.pragma('foreign_keys = ON');



export function initDatabase(): void {

    try {
      
        db.exec(schemasql);

        console.log('Base de datos inicializada correctamente');
    } catch (error) {
        console.log('Error al inicializar la base de datos :' + error);        
    }
}

export default db;