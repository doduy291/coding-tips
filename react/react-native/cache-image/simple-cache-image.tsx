import React, { useEffect, useState } from 'react'
import { Image } from 'react-native'
import { Dirs, FileSystem } from 'react-native-file-access'
import SHA1 from 'crypto-js/sha1'

type Props = {
  uri: string
  placeholder: () => JSX.Element
  style?: any
  resizeMode?: any
}

const CacheImage = ({ uri, placeholder: Placeholder, ...props }: Props) => {
  const [source, setSource] = useState<{ uri: string } | null>(null)

  useEffect(() => {
    const fetchImage = async () => {
      try {
        const cachePath = await getCachePath(uri)
        const fileExists = await FileSystem.exists(cachePath)
        if (fileExists) {
          setSource({ uri: `file://${cachePath}` })
          return
        }

        const base64data = await downloadImage(uri)
        await FileSystem.writeFile(cachePath, base64data, 'base64')
        setSource({ uri: `file://${cachePath}` })
      } catch (error) {
        console.error('Error caching image', error)
        setSource({ uri: uri }) // set the source to the original URI if an error occurs
      }
    }

    fetchImage()
  }, [uri])

  return source ? <Image source={source} {...props} /> : <Placeholder />
}

async function getCachePath(uri: string) {
  const filename = uri.substring(
    uri.lastIndexOf('/'),
    uri.indexOf('?') === -1 ? uri.length : uri.indexOf('?')
  )

  const ext =
    filename.indexOf('.') === -1
      ? '.jpg'
      : filename.substring(filename.lastIndexOf('.'))
  const sha = SHA1(uri)
  const cacheKey = `${sha}${ext}`
  const cacheDir = Dirs.CacheDir
  const cachePath = `${Dirs.CacheDir}/${cacheKey}`

  const dirExists = await FileSystem.exists(cacheDir)
  if (!dirExists) {
    await FileSystem.mkdir(cacheDir)
  }

  return cachePath
}

async function downloadImage(uri: string) {
  const response = await fetch(uri)
  const blob = await response.blob()
  const reader = new FileReader()
  const dataUrlPromise = new Promise<string>((resolve, reject) => {
    reader.onloadend = () => {
      const base64data = reader.result
      if (typeof base64data === 'string') {
        resolve(base64data.substring(base64data.indexOf(',') + 1)) // remove the `data:` scheme and the MIME type
      } else {
        reject(new Error('Failed to read blob as data URL'))
      }
    }
  })
  reader.readAsDataURL(blob)
  return await dataUrlPromise
}

export default CacheImage