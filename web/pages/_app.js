import '@/styles/globals.css';
import Navbar from '@/components/layout/Navbar';
import { QuestionsProvider } from '@/context/QuestionsContext';

export default function MyApp({ Component, pageProps }) {
  return (
    <QuestionsProvider>
      <Navbar />
      <Component {...pageProps} />
    </QuestionsProvider>
  );
}
