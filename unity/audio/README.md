### 1. Frequently played sounds

Sounds that are played in large quantities (ex. weapon sounds, footsteps, impact sounds, etc.).
<br>
Best work with the following settings (also suitable for short sounds that are under 10 seconds):
<br>

- Load Type: **Decompress on Load**
- Compression Format: **ADPCM**

(From Unity documentation) Decompress On Load: Audio files will be decompressed as soon as they are loaded. Use this option for smaller compressed sounds to avoid the performance overhead of decompressing on the fly. Be aware that decompressing Vorbis-encoded sounds on load will use about ten times more memory than keeping them compressed (for ADPCM encoding it's about 3.5 times), so don't use this option for large files.
<br>
(From Unity documentation) ADPCM: This format is useful for sounds that contain a fair bit of noise and need to be played in large quantities, such as footsteps, impacts, weapons. The compression ratio is 3.5 times smaller than PCM, but CPU usage is much lower than the MP3/Vorbis formats which makes it the preferrable choice for the aforementioned categories of sounds.

### 2. Periodical or rare playing sounds

Sounds that do not need to play frequently, for example, an announcer's voice at the beginning of the round, a timer sound at the beginning of the racing game, or basically any sound that is over 10 seconds but under 1 minute.

- Load Type: **Compressed In Memory**
- Compression Format: **ADPCM**

(From Unity documentation) Compressed In Memory: Keep sounds compressed in memory and decompress while playing. This option has a slight performance overhead (especially for Ogg/Vorbis compressed files) so only use it for bigger files where decompression on load would use a prohibitive amount of memory. The decompression is happening on the mixer thread and can be monitored in the "DSP CPU" section in the audio pane of the profiler window.

### 3. Background/Ambient sounds

Background/ambient sounds, that are over a minute long.

- Load Type: **Streaming** (or **Compressed In Memory** if you are targeting WebGL)

- Compression Format: **Vorbis**

(From Unity documentation) Vorbis/MP3: The compression results in smaller files but with somewhat lower quality compared to PCM audio. The amount of compression is configurable via the Quality slider. This format is best for medium length sound effects and music.
