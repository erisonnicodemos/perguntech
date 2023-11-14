db.createUser({
    user: 'root',
    pwd: 'admin123',
    roles: [
        {
            role: 'readWrite',
            db: 'perguntechdb'
        }
    ]
});
