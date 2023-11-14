import Link from 'next/link';
import React from 'react';

const Navbar = () => {
  return (
    <nav className="bg-white px-2 sm:px-4 py-2.5 rounded dark:bg-gray-800">
      <div className="container flex flex-wrap justify-between items-center mx-auto">
        <Link href="/" className="flex items-center text-xl font-semibold whitespace-nowrap dark:text-white">
            PergunTech
        </Link>
        <div className="flex md:order-2">
            <Link href="/create-question" className="text-white bg-purple-700 hover:bg-purple-800 focus:ring-4 focus:ring-purple-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-purple-600 dark:hover:bg-purple-700 dark:focus:ring-purple-900">
                New Question
            </Link>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
