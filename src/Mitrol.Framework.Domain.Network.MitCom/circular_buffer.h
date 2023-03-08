#pragma once

//-------------------------------------------------------------------
// Circular_Buffer (Class)
//     We will implement the buffer with a templated class so
//     the buffer can be a buffer of specified type.
//-------------------------------------------------------------------
template <class T> class CircularBuffer {
private:
	std::unique_ptr<T[]> buffer; // using a smart pointer is safer (and we don't have to implement a destructor)

	size_t head = 0;             // size_t is an unsigned long
	size_t tail = 0;
	size_t max_size;
	T empty_item; // we will use this to clear data

public:

	T& begin() const { return buffer[head]; }
	T& end() const { return buffer[tail]; }
	 

public:

	// Create a new Circular_Buffer.
	CircularBuffer<T>(size_t max_size)
		: buffer(std::unique_ptr<T[]>(new T[max_size])), max_size(max_size) {};

	// Add an item to this circular buffer.
	void enqueue(T item) {

		// if buffer is full, throw an error
		if (is_full())
			throw std::runtime_error("buffer is full");

		// insert item at back of buffer
		buffer[tail] = item;

		// increment tail
		tail = (tail + 1) % max_size;
	}

	// Remove an item from this circular buffer and return it.
	T dequeue() {

		// if buffer is empty, throw an error
		if (is_empty())
			throw std::runtime_error("buffer is empty");

		// get item at head
		T item = buffer[head];

		// set item at head to be empty
		T empty;
		buffer[head] = empty_item;

		// move head foward
		head = (head + 1) % max_size;

		// return item
		return item;
	}

	// Return the item at the front of this circular buffer.
	T front() { return buffer[head]; }

	// Return true if this circular buffer is empty, and false otherwise.
	bool is_empty() { return head == tail; }

	// Return true if this circular buffer is full, and false otherwise.
	bool is_full() { return tail == (head - 1) % max_size; }

	// Return the size of this circular buffer.
	size_t size() {
		if (tail >= head)
			return tail - head;
		return max_size - head - tail;
	}
};


class LogBuffer : CircularBuffer<std::string>
{


public:
	LogBuffer(size_t max_size) : CircularBuffer(max_size) { }

	// Add an item to this circular buffer.
	void enqueue(const char* text)
	{
		std::string str(text);
		CircularBuffer::enqueue(str);
	}

};

template <typename T, typename C, size_t const Size>
class LogBufferIterator
{
public:
	LogBufferIterator(C& collection, size_t const index) :
		index(index), collection(collection)
	{ }

	bool operator!= (LogBufferIterator const & other) const
	{
		return index != other.index;
	}

	T const & operator* () const
	{
		return collection.GetAt(index);
	}

	LogBufferIterator const & operator++ ()
	{
		++index;
		return *this;
	}

private:
	size_t   index;
	C&       collection;
};