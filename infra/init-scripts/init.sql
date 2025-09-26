CREATE TABLE IF NOT EXISTS users (
id serial PRIMARY KEY,
full_name text NOT NULL,
email text,
created_at timestamptz DEFAULT now()
);


INSERT INTO users (full_name, email) VALUES
('Alice Example', 'alice@example.com')
ON CONFLICT DO NOTHING;