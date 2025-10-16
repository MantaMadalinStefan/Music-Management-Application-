from flask import Flask, render_template, request, redirect, url_for, flash
import mysql.connector

app = Flask(__name__)
app.secret_key = 'cheie_secreta'

# Conexiune la baza de date
def get_db_connection():
    return mysql.connector.connect(
        host="127.0.0.1",
        user="root",
        password="root",
        database="music"
    )

# Pagina principala
@app.route('/')
def home():
    return render_template('index.html', title="Manager Muzical")

# Gestionare muzicieni
@app.route('/musicians', methods=['GET', 'POST'])
def manage_musicians():
    db = get_db_connection()
    cursor = db.cursor()
    if request.method == 'POST':
        name = request.form.get('name')
        genre = request.form.get('genre')
        if name and genre:
            cursor.execute("INSERT INTO Musicians (nume, gen_muzical) VALUES (%s, %s)", (name, genre))
            db.commit()
            flash('Muzicianul a fost adăugat cu succes!')
        else:
            flash('Toate câmpurile sunt obligatorii!', 'error')
    cursor.execute("SELECT * FROM Musicians")
    musicians = cursor.fetchall()
    db.close()
    return render_template('musicians.html', musicians=musicians, title="Manager Muzical")

@app.route('/modify_musician/<int:id>', methods=['GET', 'POST'])
def modify_musician(id):
    db = get_db_connection()
    cursor = db.cursor()
    if request.method == 'POST':
        name = request.form.get('name')
        genre = request.form.get('genre')
        if name and genre:
            cursor.execute("UPDATE Musicians SET nume=%s, gen_muzical=%s WHERE id=%s", (name, genre, id))
            db.commit()
            flash('Muzicianul a fost modificat cu succes!')
            return redirect(url_for('manage_musicians'))
    cursor.execute("SELECT * FROM Musicians WHERE id=%s", (id,))
    musician = cursor.fetchone()
    db.close()
    return render_template('modify_musician.html', musician=musician, title="Manager Muzical")

@app.route('/delete_musician/<int:id>')
def delete_musician(id):
    db = get_db_connection()
    cursor = db.cursor()
    cursor.execute("DELETE FROM Albums_Musicians WHERE id_muzician=%s", (id,))
    cursor.execute("DELETE FROM Musicians WHERE id=%s", (id,))
    db.commit()
    db.close()
    flash('Muzicianul a fost șters cu succes!')
    return redirect(url_for('manage_musicians'))

# Gestionare albume
@app.route('/albums', methods=['GET', 'POST'])
def manage_albums():
    db = get_db_connection()
    cursor = db.cursor()
    if request.method == 'POST':
        title = request.form.get('title')
        year = request.form.get('year')
        if title and year:
            cursor.execute("INSERT INTO Albums (titlu, an_lansare) VALUES (%s, %s)", (title, year))
            db.commit()
            flash('Albumul a fost adăugat cu succes!')
        else:
            flash('Toate câmpurile sunt obligatorii!', 'error')
    cursor.execute("SELECT * FROM Albums")
    albums = cursor.fetchall()
    db.close()
    return render_template('albums.html', albums=albums, title="Manager Muzical")

@app.route('/modify_album/<int:id>', methods=['GET', 'POST'])
def modify_album(id):
    db = get_db_connection()
    cursor = db.cursor()
    if request.method == 'POST':
        title = request.form.get('title')
        year = request.form.get('year')
        if title and year:
            cursor.execute("UPDATE Albums SET titlu=%s, an_lansare=%s WHERE id=%s", (title, year, id))
            db.commit()
            flash('Albumul a fost modificat cu succes!')
            return redirect(url_for('manage_albums'))
    cursor.execute("SELECT * FROM Albums WHERE id=%s", (id,))
    album = cursor.fetchone()
    db.close()
    return render_template('modify_album.html', album=album, title="Manager Muzical")

@app.route('/delete_album/<int:id>')
def delete_album(id):
    db = get_db_connection()
    cursor = db.cursor()
    cursor.execute("DELETE FROM Albums_Musicians WHERE id_album=%s", (id,))
    cursor.execute("DELETE FROM Albums WHERE id=%s", (id,))
    db.commit()
    db.close()
    flash('Albumul a fost șters cu succes!')
    return redirect(url_for('manage_albums'))

from mysql.connector.errors import IntegrityError

@app.route('/relationships', methods=['GET', 'POST'])
def manage_relationships():
    db = get_db_connection()
    cursor = db.cursor()
    
    if request.method == 'POST':
        musician_id = request.form.get('musician_id')
        album_id = request.form.get('album_id')

        if musician_id and album_id:
            # Verifică dacă relația deja există
            cursor.execute("SELECT * FROM Albums_Musicians WHERE id_muzician = %s AND id_album = %s", (musician_id, album_id))
            existing_relationship = cursor.fetchone()

            if existing_relationship:
                flash('Albumul deja este compus de acel artist.', 'error')
            else:
                try:
                    cursor.execute("INSERT INTO Albums_Musicians (id_muzician, id_album) VALUES (%s, %s)", (musician_id, album_id))
                    db.commit()
                    flash('Relația a fost creată cu succes!')
                except IntegrityError:
                    flash('Albumul deja este compus de acel artist.', 'error')

    # Obține lista de relații, muzicieni și albume
    cursor.execute("SELECT M.id, A.id, M.nume, A.titlu FROM Albums_Musicians AM JOIN Musicians M ON AM.id_muzician = M.id JOIN Albums A ON AM.id_album = A.id")
    relationships = cursor.fetchall()
    cursor.execute("SELECT * FROM Musicians")
    musicians = cursor.fetchall()
    cursor.execute("SELECT * FROM Albums")
    albums = cursor.fetchall()
    
    db.close()
    return render_template('relationships.html', musicians=musicians, albums=albums, relationships=relationships, title="Manager Muzical")


@app.route('/delete_relationship/<int:musician_id>/<int:album_id>')
def delete_relationship(musician_id, album_id):
    db = get_db_connection()
    cursor = db.cursor()
    cursor.execute("DELETE FROM Albums_Musicians WHERE id_muzician=%s AND id_album=%s", (musician_id, album_id))
    db.commit()
    db.close()
    flash('Relația a fost ștearsă cu succes!')
    return redirect(url_for('manage_relationships'))

if __name__ == '__main__':
    app.run(debug=True)
