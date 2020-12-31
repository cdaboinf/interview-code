using System;
using System.Collections;

namespace interview_code.CompanyTechInterviews
{
    public class PetalTechScreening
    {
        public PetalTechScreening() { }

        public DoubleLink ReverseLinkedList(DoubleLink head)
        {
            DoubleLink prev = null;
            DoubleLink curr = head;

            while (curr != null)
            {
                DoubleLink next = curr.NextLink;
                curr.NextLink = prev;
                prev = curr;
                curr = next;
            }
            return prev;
        }

        public bool DetectCycle(DoubleLink head)
        {
            DoubleLink fast = head, slow = head;
            while (fast != null)
            {
                fast = fast.NextLink;
                if (fast == slow)
                {
                    return true;
                }
                if (fast != null)
                {
                    fast = fast.NextLink;
                    if (fast == slow)
                        return true;
                }
                slow = slow.NextLink;
            }
            return false;
        }
    }

    public class LRUCache
    {    // Maps keys to nodes
        Hashtable map;

        // Linked List variables
        DoubleLink head;
        DoubleLink tail;

        // Maximum nodes the cache can hold.
        int capacity;

        public LRUCache(int capacity)
        {
            this.map = new Hashtable();
            this.capacity = capacity;
        }

        // Read a value from cache.
        public DoubleLink read(int key)
        {
            DoubleLink node = (DoubleLink)map[key];
            if (node == null)
            {
                return null;
            }
            remove(key); // remove from linked hash table
            add(node.Value); // add back to front
            return node;
        }

        // Write a value to cache.
        public void write(int value)
        {
            if (map.Count == capacity)
            {
                // cache is full, evict the head
                remove(head.Value);
            }

            // In this implementation, we create a new node every time.
            // If you want, you can also move the same node to the end.
            add(value);
        }

        // Removed a node from the Linked Hash Table
        private void remove(int value)
        {
            if (!map.ContainsKey(value))
            {
                return;
            }

            DoubleLink toRemove = (DoubleLink)map[value];

            //  (prev)1<->2<->3(next)
            //  1-()->3
            if (toRemove.PreviousLink != null)
            {
                var prevNode = toRemove.PreviousLink;
                prevNode.NextLink = toRemove.NextLink;
            }
            //  (prev)1<->2<->3(next)
            //  1<-()-3
            if (toRemove.NextLink != null)
            {
                var nextNode = toRemove.NextLink;
                nextNode.PreviousLink = toRemove.PreviousLink;
            }

            if (toRemove == head)
            {
                head = toRemove.NextLink;
            }

            if (toRemove == tail)
            {
                tail = toRemove.PreviousLink;
            }

            map.Remove(value);
        }

        // Add a node to the end of the Linked Hash Table
        private void add(int value)
        {
            // linked-list
            DoubleLink newNode = new DoubleLink(value);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                tail.NextLink = newNode;
                newNode.PreviousLink = tail;
            }
            tail = newNode;

            // map
            if (!map.ContainsKey(value))
            {
                map.Add(value, newNode);
            }
            else
            {
                map[value] = newNode;
            }
        }
    }

    public class DoubleLink
    {
        public int Value { get; set; }
        public DoubleLink PreviousLink { get; set; }
        public DoubleLink NextLink { get; set; }

        public DoubleLink(int value)
        {
            Value = value;
        }
    }

    public class DoubleLinkedList
    {
        private DoubleLink _first;
        public bool IsEmpty
        {
            get
            {
                return _first == null;
            }
        }
        public DoubleLinkedList()
        {
            _first = null;
        }

        public DoubleLink Insert(int value)
        {
            // Creates a link, sets its link to the first item and then makes this the first item in the list.
            DoubleLink link = new DoubleLink(value);
            link.NextLink = _first;
            if (_first != null)
                _first.PreviousLink = link;
            _first = link;
            return link;
        }

        public DoubleLink Delete()
        {
            // Gets the first item, and sets it to be the one it is linked to
            DoubleLink temp = _first;
            if (_first != null)
            {
                _first = _first.NextLink;
                if (_first != null)
                    _first.PreviousLink = null;
            }
            return temp;
        }

        public void InsertAfter(DoubleLink link, int value)
        {
            if (link == null)
                return;
            DoubleLink newLink = new DoubleLink(value);
            newLink.PreviousLink = link;
            // Update the 'after' link's next reference, so its previous points to the new one
            if (link.NextLink != null)
                link.NextLink.PreviousLink = newLink;
            // Steal the next link of the node, and set the after so it links to our new one
            newLink.NextLink = link.NextLink;
            link.NextLink = newLink;
        }
    }
}
